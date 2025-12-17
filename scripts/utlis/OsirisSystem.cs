using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using Godot;
using Prion;

namespace Osiris
{
    public static class OsirisSystem
    {
        static Func<string, string> Reader = filepath =>
        {
            using var file = FileAccess.Open(filepath, FileAccess.ModeFlags.Read);
            return file.GetAsText();
        };
        static Action<string, string> Writer = (filepath, contents) =>
        {
            using var file = FileAccess.Open(filepath, FileAccess.ModeFlags.Read);
            file.StoreString(contents);
        };
        static Func<string, bool> Checker = FileAccess.FileExists;
        static readonly Dictionary<string, string> MockFilesystem = [];
        static readonly List<string> ErrorLog = [];
        static readonly List<string> MessageLog = [];
        static bool InTestMode = false;
        public static readonly PrionSchemaManager SchemaManager = new();
        public static void LoadSchemas()
        {
            (Type, string)[] schemaFiles = [
                (typeof(ActorData), "actor_schema.json"),
                (typeof(AssetLogData), "asset_log_schema.json"),
                (typeof(HandoutData), "handout_schema.json"),
                (typeof(HandoutData), "handout_schema.json"),
            ];
            foreach (var (type, filename) in schemaFiles)
            {
                string schemaSrc = ReadFile($"res://scripts/schemas/{filename}");
                var jsonNode = JsonNode.Parse(schemaSrc);
                if(!PrionNode.TryFromJson(jsonNode, out PrionNode prionNode)) return;
                if(!PrionSchema.TryFromNode(prionNode, out PrionSchema schema, out string _)) return;
                SchemaManager.RegisterSchema(type, schema);
            }
        }
        public static void EnterTestMode()
        {
            MockFilesystem.Clear();
            ErrorLog.Clear();
            MessageLog.Clear();
            if(InTestMode) return;
            InTestMode = true;
            Reader = filepath =>
            {
                if(TestUtils.FileExists(ConvertPath(filepath))) return TestUtils.ReadFile(ConvertPath(filepath));
                if(MockFilesystem.TryGetValue(filepath, out string contents)) return contents;
                return "";
            };
            Checker = filepath =>
            {
                if(TestUtils.FileExists(ConvertPath(filepath))) return true;
                if(MockFilesystem.ContainsKey(filepath)) return true;
                return false;
            };
            Writer = (filepath, contents) =>
            {
                MockFilesystem.Add(filepath, contents);
            };
        }
        public static string ConvertPath(string filepath)
        {
            if(filepath.StartsWith("res://")) filepath = filepath[6..];
            return filepath;
        }
        public static string ReadFile(string filename)
        {
            return Reader(filename);
        }
        public static void WriteFile(string filename, string contents)
        {
            Writer(filename, contents);
        }
        public static bool FileExists(string filename)
        {
            return Checker(filename);
        }
        public static void ReportError<T>(params T[] messages)
        {
            string message = "";
            foreach (var m in messages)
            {
                message += m;
            }
            ErrorLog.Add(message);
            if(!InTestMode) GD.PrintErr(message);
        }
        public static bool HasErrors()
        {
            return ErrorLog.Count > 0;
        }
        public static string GetErrors()
        {
            return string.Join("\n", ErrorLog);
        }
        public static void Log<T>(params T[] messages)
        {
            string message = "";
            foreach (var m in messages)
            {
                message += m;
            }
            MessageLog.Add(message);
            if(!InTestMode) GD.Print(message);
        }
        public static string GetLogs()
        {
            return string.Join("\n", ErrorLog);
        }
    }
}
