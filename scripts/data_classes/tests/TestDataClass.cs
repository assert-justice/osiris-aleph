using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prion;
using Prion.Schema;

namespace Osiris.DataClass.Tests;

// [TestClass]
public abstract class TestDataClass<T> where T : class, IDataClass
{
    protected PrionNode Data;
    protected string ExampleString;
    protected JsonNode ExampleJson;
    protected PrionNode ExampleNode;
    readonly Type DataType;
    readonly string Name;
    readonly List<(Type, string)> Dependencies = [];
    protected TestDataClass(string name){
        Name = name;
        DataType = typeof(T);
    }

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
    protected static void AddDependency(string name, Type type)
    {
        OsirisSystem.LoadSchema(type, name);
    }
    protected void LoadAndValidate()
    {
        OsirisSystem.LoadSchema(DataType, Name);
        string path = $"scripts/schemas/{Name}_example.json";
        if (!OsirisSystem.FileExists(path))
        {
            TestUtils.Fail($"Failed to find example with path '{path}'.");
        }
        ExampleString = OsirisSystem.ReadFile(path);
        ExampleJson = JsonNode.Parse(ExampleString);
        if(!PrionNode.TryFromJson(ExampleJson, out ExampleNode))
        {
            TestUtils.Fail($"Failed to parse json at path '{path}'. Error: {ExampleNode}");
        }
        if(!IDataClass.TryFromNode(ExampleNode, out T data)) TestUtils.Fail();
        ExampleNode = data.ToNode();
        if(!PrionSchemaManager.Validate(DataType, ExampleNode, out string error))
        {
            TestUtils.Fail(error);
        }
    }
}
