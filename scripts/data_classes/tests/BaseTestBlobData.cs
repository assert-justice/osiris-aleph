using System;
using System.Text.Json.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prion.Node;
using Prion.Schema;

namespace Osiris.DataClass.Tests;

public abstract class BaseTestBlobData<T>(string name) where T : BlobData, ITryFromNode<T>
{
    readonly string Name = name;
    readonly Type DataType = typeof(T);
    [TestInitialize]
    public void Init()
    {
        OsirisSystem.EnterTestMode();
    }
    [TestCleanup]
    public void Cleanup()
    {
        if (OsirisSystem.HasErrors())
        {
            TestUtils.Fail();
        }
    }
    protected PrionNode Load(string exampleName)
    {
        string path = $"scripts/schemas/{exampleName}_example.json";
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
        return exampleNode;
    }
    protected void LoadAndValidate(string exampleName = null)
    {
        var data = Load(exampleName ?? Name);
        Validate(data);
    }
    protected void Validate(T data)
    {
        if(!PrionSchemaManager.Validate(DataType, data.ToNode(), out string error))
        {
            TestUtils.Fail(error);
        }
    }
    protected void Validate(PrionNode prionNode)
    {
        if(!PrionSchemaManager.Validate(DataType, prionNode, out string error))
        {
            TestUtils.Fail(error);
        }
    }
}
