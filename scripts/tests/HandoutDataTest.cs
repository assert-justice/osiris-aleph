using System.Text.Json.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class HandoutDataTest
{
    [TestMethod]
    public void Empty()
    {
        string str = TestUtils.ReadFile("scripts/tests/example_files/handout_data/empty.json");
        var exampleNode = JsonNode.Parse(str);
        string exampleStr = exampleNode.ToJsonString();
        HandoutData handout = new([])
        {
            Id = RojaUtils.ObjGetGuid(exampleNode.AsObject(), "handout_id")
        };
        var testNode = handout.Serialize();
        string testString = testNode.ToJsonString();
        Assert.IsTrue(exampleStr == testString);
    }
}
