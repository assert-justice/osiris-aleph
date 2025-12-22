
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Osiris;
using Prion;

public static class TestUtils
{
    public static string ReadFile(string filename)
    {
        string filepath = $"../../../../../{filename}";
        return File.ReadAllText(filepath);
    }
    public static bool FileExists(string filename)
    {
        string filepath = $"../../../../../{filename}";
        return File.Exists(filepath);
    }
    public static bool DirExists(string path)
    {
        path = $"../../../../../{path}";
        return Directory.Exists(path);
    }
    public static string[] DirGetFilenames(string path)
    {
        path = $"../../../../../{path}";
        return Directory.GetFiles(path);
    }
    public static void Fail(string message = null)
    {
        if(message is not null) OsirisSystem.ReportError(message);
        Assert.Fail(OsirisSystem.GetErrors());
    }
}
