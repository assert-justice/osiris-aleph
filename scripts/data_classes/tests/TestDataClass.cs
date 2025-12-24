using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prion.Node;
using Prion.Schema;

namespace Osiris.DataClass.Tests;

public abstract class TestDataClass<T>(string name) where T : class, IDataClass<T>
{
    readonly string Name = name;
    readonly Type DataType = typeof(T);
    protected PrionNode Data;
    protected string ExampleString;
    protected JsonNode ExampleJson;
    protected PrionNode ExampleNode;
    readonly List<(Type, string)> Dependencies = [];

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
    protected void AddDependency(string name, Type type)
    {
        Dependencies.Add((type, name));
    }
    protected T Load(string exampleName)
    {
        string path = $"scripts/schemas/{exampleName}_example.json";
        if (!OsirisSystem.FileExists(path))
        {
            TestUtils.Fail($"Failed to find example with path '{path}'.");
        }
        ExampleString = OsirisSystem.ReadFile(path);
        ExampleJson = JsonNode.Parse(ExampleString);
        if(!PrionNode.TryFromJson(ExampleJson, out ExampleNode, out string error))
        {
            TestUtils.Fail($"Failed to parse json at path '{path}'. Error: {error}");
        }
        if(!T.TryFromNode(ExampleNode, out T data)) TestUtils.Fail();
        return data;
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
}
