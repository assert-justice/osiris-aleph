using System;
using System.Collections.Generic;
using Godot;

namespace Osiris
{
    public static class OsirisFileAccess
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
        static bool InTestMode = false;
        public static void EnterTestMode()
        {
            if(InTestMode) return;
            // Reader = TestUtils.ReadFile;
            // Checker = TestUtils.FileExists;
            InTestMode = true;
            Reader = filepath =>
            {
                if(TestUtils.FileExists(filepath)) return TestUtils.ReadFile(filepath);
                if(MockFilesystem.TryGetValue(filepath, out string contents)) return contents;
                return "";
            };
            Checker = filepath =>
            {
                if(TestUtils.FileExists(filepath)) return true;
                if(MockFilesystem.ContainsKey(filepath)) return true;
                return false;
            };
            Writer = (filepath, contents) =>
            {
                MockFilesystem.Add(filepath, contents);
            };
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
    }
}
