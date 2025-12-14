
using System;
using Godot;

namespace Osiris
{
    public static class OsirisFileAccess
    {
        static Func<string, string> Reader = filepath =>
        {
            using var file = FileAccess.Open("res://" + filepath, FileAccess.ModeFlags.Read);
            return file.GetAsText();
        };
        static Action<string, string> Writer;
        static Func<string, bool> Checker = FileAccess.FileExists;
        static bool InTestMode = false;
        public static void EnterTestMode()
        {
            if(InTestMode) return;
            Reader = TestUtils.ReadFile;
            Checker = TestUtils.FileExists;
            InTestMode = true;
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
