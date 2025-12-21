using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
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
    [TestMethod]
    public void Mock()
    {
        int trials = 100;
        for (int idx = 0; idx < trials; idx++)
        {
            var stamp = MockStampImage(Guid.NewGuid());
            Validate(stamp);
        }
        for (int idx = 0; idx < trials; idx++)
        {
            var stamp = MockStampText(Guid.NewGuid());
            Validate(stamp);
        }
        for (int idx = 0; idx < trials; idx++)
        {
            var stamp = MockStampToken(Guid.NewGuid());
            Validate(stamp);
        }
    }
    public static StampData MockRandom()
    {
        Func<Guid, StampData>[] factories = [
            MockStampImage,
            MockStampText,
            MockStampToken,
        ];
        return factories[MockData.Rng.Next(factories.Length)](Guid.NewGuid());
    }
    static void MockCommon(StampData data)
    {
        data.ControlledBy = MockData.GetRandomSet(Guid.NewGuid, MockData.Rng.Next(4));
        var pos = MockData.GetRandomVector2I(-100, 100, -100, 100);
        var size = MockData.GetRandomVector2I(1, 5, 1, 5);
        data.Rect = new(pos, size);
        data.Angle = MockData.GetRandomFloat() * (float)Math.PI * 2;
        data.VisionRadius = MockData.GetRandomFloat(5, 50);
        data.HasVision = MockData.GetRandomBool();
    }
    public static StampDataImage MockStampImage(Guid id)
    {
        StampDataImage data = new(id);
        MockCommon(data);
        data.ImageFilename = MockData.GetRandomIdent();
        data.StretchMode = (StampDataImage.ImageStretchMode)MockData.Rng.Next(6);
        return data;
    }
    public static StampDataText MockStampText(Guid id)
    {
        StampDataText data = new(id);
        MockCommon(data);
        data.Text = MockData.GetRandomText(1000);
        data.FontFilename = MockData.GetRandomIdent();
        data.FontSize = MockData.GetRandomIdent();
        data.Margins[0] = MockData.GetRandomIdent();
        data.Margins[1] = MockData.GetRandomIdent();
        data.Margins[2] = MockData.GetRandomIdent();
        data.Margins[3] = MockData.GetRandomIdent();
        data.WrapMode = (StampDataText.TextWrapMode)MockData.Rng.Next(4);
        return data;
    }
    public static StampDataToken MockStampToken(Guid id)
    {
        StampDataToken data = new(id);
        MockCommon(data);
        data.ActorId = Guid.NewGuid();
        data.IsUnique = MockData.GetRandomBool();
        return data;
    }
    static void Validate<T>(T data) where T : StampData
    {
        var exampleNode = data.ToNode();
        if(!PrionSchemaManager.Validate(typeof(T), exampleNode, out string error))
        {
            TestUtils.Fail(error);
        }
    }
    static void LoadAndValidate<T>(string stampType) where T : StampData
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
        Validate(data);
    }
}
