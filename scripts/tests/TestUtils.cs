
using System.IO;

public static class TestUtils
{
    public static string ReadExample(string filename)
    {
        string filepath = $"../../../../../scripts/tests/example_files/{filename}";
        return File.ReadAllText(filepath);
    }
}
