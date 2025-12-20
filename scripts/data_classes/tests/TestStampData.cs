using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prion.Node;
using Prion.Schema;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestStampData
{
    [TestInitialize]
    public void Init()
    {
        OsirisSystem.EnterTestMode();
        OsirisSystem.LoadSchema(typeof(StampDataImage), "stamp");
        OsirisSystem.LoadSchema(typeof(StampDataText), "stamp");
        OsirisSystem.LoadSchema(typeof(StampDataToken), "stamp");
    }
    [TestCleanup]
    public void Cleanup()
    {
        if (OsirisSystem.HasErrors())
        {
            TestUtils.Fail();
        }
    }

    [TestMethod]
    public void LoadAndValidateImage()
    {
        LoadAndValidate<StampDataImage>("image");
    }
    [TestMethod]
    public void LoadAndValidateText()
    {
        LoadAndValidate<StampDataText>("text");
    }
    [TestMethod]
    public void LoadAndValidateToken()
    {
        LoadAndValidate<StampDataToken>("token");
    }
    public static void LoadAndValidate<T>(string stampType) where T : StampData
    {
        string path = $"scripts/schemas/stamp_{stampType}_example.json";
        if (!OsirisSystem.FileExists(path))
        {
            TestUtils.Fail($"Failed to find example with path '{path}'.");
        }
        string exampleString = OsirisSystem.ReadFile(path);
        JsonNode exampleJson = JsonNode.Parse(exampleString);
        if(!PrionNode.TryFromJson(exampleJson, out PrionNode exampleNode, out string error))
        {
            TestUtils.Fail($"Failed to parse json at path '{path}'. Error: {error}");
        }
        if(!StampData.TryFromNode(exampleNode, out T data)) TestUtils.Fail();
        exampleNode = data.ToNode();
        // TestUtils.Fail((data is null).ToString());
        if(!PrionSchemaManager.Validate(typeof(T), exampleNode, out error))
        {
            TestUtils.Fail(error);
        }
    }
}
