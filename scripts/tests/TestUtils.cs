
using System.IO;

public static class TestUtils
{
    public static string ReadFile(string filename)
    {
        string filepath = $"../../../../../{filename}";
        return File.ReadAllText(filepath);
    }
}
