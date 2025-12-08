using System.Text.Json.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class BlockerDataTest
{
    [TestMethod]
    public void Empty()
    {
        string str = TestUtils.ReadExample("blocker_data/blocker_data_empty.json");
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
        string str = TestUtils.ReadExample("blocker_data/blocker_data_wall.json");
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
    [TestMethod]
    public void Open()
    {
        string str = TestUtils.ReadExample("blocker_data/blocker_data_door_open.json");
        var exampleNode = JsonNode.Parse(str);
        string exampleStr = exampleNode.ToJsonString();
        BlockerData blocker = new([])
        {
            End = new(10, 0),
            Status = BlockerStatus.Open,
        };
        var testNode = blocker.Serialize();
        string testString = testNode.ToJsonString();
        Assert.IsTrue(exampleStr == testString);
    }
    [TestMethod]
    public void Closed()
    {
        string str = TestUtils.ReadExample("blocker_data/blocker_data_door_closed.json");
        var exampleNode = JsonNode.Parse(str);
        string exampleStr = exampleNode.ToJsonString();
        BlockerData blocker = new([])
        {
            End = new(10, 0),
            Status = BlockerStatus.Closed,
        };
        var testNode = blocker.Serialize();
        string testString = testNode.ToJsonString();
        Assert.IsTrue(exampleStr == testString);
    }
    [TestMethod]
    public void Locked()
    {
        string str = TestUtils.ReadExample("blocker_data/blocker_data_door_locked.json");
        var exampleNode = JsonNode.Parse(str);
        string exampleStr = exampleNode.ToJsonString();
        BlockerData blocker = new([])
        {
            End = new(10, 0),
            Status = BlockerStatus.Locked,
        };
        var testNode = blocker.Serialize();
        string testString = testNode.ToJsonString();
        Assert.IsTrue(exampleStr == testString);
    }
    [TestMethod]
    public void WallGlass()
    {
        string str = TestUtils.ReadExample("blocker_data/blocker_data_wall_glass.json");
        var exampleNode = JsonNode.Parse(str);
        string exampleStr = exampleNode.ToJsonString();
        BlockerData blocker = new([])
        {
            End = new(10, 0),
            Opaque = false,
        };
        var testNode = blocker.Serialize();
        string testString = testNode.ToJsonString();
        Assert.IsTrue(exampleStr == testString);
    }
    [TestMethod]
    public void Portcullis()
    {
        string str = TestUtils.ReadExample("blocker_data/blocker_data_portcullis.json");
        var exampleNode = JsonNode.Parse(str);
        string exampleStr = exampleNode.ToJsonString();
        BlockerData blocker = new([])
        {
            End = new(10, 0),
            Status = BlockerStatus.Locked,
            Opaque = false,
            BlocksProjectiles = false,
        };
        var testNode = blocker.Serialize();
        string testString = testNode.ToJsonString();
        Assert.IsTrue(exampleStr == testString);
    }
}
