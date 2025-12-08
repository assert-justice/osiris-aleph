using System;
using System.IO;
using System.Text.Json.Nodes;
using Godot;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class BlockerDataTest
{
    [TestMethod]
    public void Empty()
    {
        string str = TestUtils.ReadExample("blocker_data_empty.json");
        var exampleNode = JsonNode.Parse(str);
        string exampleStr = exampleNode.ToJsonString();
        BlockerData blocker = new([]);
        var testNode = blocker.Serialize();
        string testString = testNode.ToJsonString();
        Assert.IsTrue(exampleStr == testString);
    }

    [TestMethod]
    public void Wall()
    {
        string str = TestUtils.ReadExample("blocker_data_wall.json");
        var exampleNode = JsonNode.Parse(str);
        string exampleStr = exampleNode.ToJsonString();
        BlockerData blocker = new([])
        {
            End = new(10, 0),
        };
        var testNode = blocker.Serialize();
        string testString = testNode.ToJsonString();
        Assert.IsTrue(exampleStr == testString);
    }
}
