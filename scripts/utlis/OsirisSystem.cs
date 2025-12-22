using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using Godot;
using Osiris.DataClass;
using Prion.Node;
using Prion.Schema;

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
        public static void LoadAllSchemas()
        {
            (Type, string)[] schemaFiles = [
                (typeof(ActorData), "actor"),
                (typeof(AssetLogData), "asset_log"),
                (typeof(BlockerData), "blocker"),
                (typeof(HandoutData), "handout"),
                (typeof(LayerData), "layer"),
                (typeof(MapData), "map"),
                (typeof(SessionData), "session"),
                (typeof(StampDataImage), "stamp"),
                (typeof(StampDataText), "stamp"),
                (typeof(StampDataToken), "stamp"),
                (typeof(TileGroupData), "tile_group"),
                (typeof(UserData), "user"),
            ];
            foreach (var (type, name) in schemaFiles)
            {
                LoadSchema(type, name);
            }
        }
        static void LoadSchema(Type type, string name)
        {
            string path = $"res://scripts/schemas/{name}_schema.json";
            if (!FileExists(path))
            {
                ReportError($"Invalid path '{path}'");
                return;
            }
            string schemaSrc = ReadFile(path);
            var jsonNode = JsonNode.Parse(schemaSrc);
            if(!PrionNode.TryFromJson(jsonNode, out PrionNode prionNode, out string error))
            {
                ReportError($"Failed to parse json at path '{path}'. Error: {error}");
                return;
            }
            if(!PrionSchema.TryFromPrionNode(prionNode, out PrionSchema prionSchema, out error))
            {
                ReportError(error);
                return;
            }
            PrionSchemaManager.RegisterSchema(prionSchema, type);
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
            LoadAllSchemas();
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
            else Console.WriteLine(message);
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
            else Console.WriteLine(message);
        }
        public static string GetLogs()
        {
            return string.Join("\n", ErrorLog);
        }
    }
}
