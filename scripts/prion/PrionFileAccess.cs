
using System;
using System.IO;

namespace Prion
{
    public static class PrionFileAccess
    {
        static Func<string, string> Reader;
        static Action<string, string> Writer;
        static Func<string, bool> Checker;
        public static void SetReader(Func<string, string> reader)
        {
            Reader = reader;
        }
        public static void SetWriter(Action<string, string> writer)
        {
            Writer = writer;
        }
        public static void SetChecker(Func<string, bool> checker)
        {
            Checker = checker;
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
