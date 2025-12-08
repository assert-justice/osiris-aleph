using System.Text.Json.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ActorDataTest
{
    [TestMethod]
    public void Empty()
    {
        string str = TestUtils.ReadExample("actor_data/empty.json");
        var exampleNode = JsonNode.Parse(str);
        string exampleStr = exampleNode.ToJsonString();
        ActorData actor = new([])
        {
            Id = JsonUtils.ObjGetGuid(exampleNode.AsObject(), "actor_id")
        };
        var testNode = actor.Serialize();
        string testString = testNode.ToJsonString();
        Assert.IsTrue(exampleStr == testString);
    }
}
