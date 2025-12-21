using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prion.Node;
using Prion.Schema;

namespace Osiris.DataClass.Tests;

public abstract class TestDataClass<T> where T : class, IDataClass<T>
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
        // OsirisSystem.LoadSchema(DataType, Name);
        // foreach (var (type, name) in Dependencies)
        // {
        //     OsirisSystem.LoadSchema(type, name);
        // }
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
    protected void LoadAndValidate()
    {
        //OsirisSystem.LoadSchema(DataType, Name);
        string path = $"scripts/schemas/{Name}_example.json";
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
        ExampleNode = data.ToNode();
        if(!PrionSchemaManager.Validate(DataType, ExampleNode, out error))
        {
            TestUtils.Fail(error);
        }
    }
    protected void Validate(T data)
    {
        if(!PrionSchemaManager.Validate(DataType, data.ToNode(), out string error))
        {
            TestUtils.Fail(error);
        }
    }
    // public virtual T Mock()
    // {
    //     return default;
    // }
    // protected void MockAndValidate(int trials)
    // {
    //     for (int idx  = 0; idx  < trials; idx ++)
    //     {
    //         Validate(Mock());
    //     }
    // }
}
