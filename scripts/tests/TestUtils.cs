
using System.IO;

public static class TestUtils
{
    public static string ReadExample(string filename)
    {
        string filepath = $"../../../../../scripts/tests/example_files/{filename}";
        return File.ReadAllText(filepath);
    }
    public static bool FileExists(string filename)
    {
        string filepath = $"../../../../../scripts/tests/example_files/{filename}";
        return File.Exists(filepath);
    }
    public static void WriteSnapshot(string filename, string contents, bool overwrite = false)
    {
        string filepath = $"../../../../../scripts/tests/example_files/{filename}";
        if(!overwrite && File.Exists(filepath)) return;
        File.WriteAllText(filepath, contents);
    }
}