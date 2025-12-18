using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestBlockerData : TestDataClass<BlockerData>
{
    public TestBlockerData() : base("blocker"){}

    [TestMethod]
    public void LoadAndValidateBlocker()
    {
        LoadAndValidate();
        // foreach (var (name, schema) in OsirisSystem.SchemaManager.SchemasByName)
        // {
        //     OsirisSystem.ReportError($"name: {name}, schema: {schema}");
        // }
        // TestUtils.Fail("wtf");
        // string snapshotString = OsirisSystem.ReadFile("scripts/schemas/blocker_example.json");
        // var snapshotJson = JsonNode.Parse(snapshotString);
        // snapshotString = snapshotJson.ToJsonString();
        // if(!PrionNode.TryFromJson(snapshotJson, out PrionNode prionNode))
        // {
        //     if(prionNode is PrionError prionError)
        //     {
        //         OsirisSystem.ReportError(string.Join("\n", prionError.Messages));
        //     }
        //     TestUtils.Fail($"Unable to convert json: {snapshotJson}");
        // }
        // if(!OsirisSystem.SchemaManager.Validate<BlockerData>(prionNode, out string error))
        // {
        //     TestUtils.Fail(error);
        // }
        // if(!IDataClass.TryFromNode(prionNode, out BlockerData blocker)) TestUtils.Fail();
        // prionNode = blocker.ToNode();
        // if(!OsirisSystem.SchemaManager.Validate<BlockerData>(prionNode, out error))
        // {
        //     TestUtils.Fail(error);
        // }
        // if(prionNode.ToJson().ToJsonString() != snapshotString)
        // {
        //     OsirisSystem.ReportError(prionNode.ToJson().ToJsonString());
        //     OsirisSystem.ReportError(snapshotString);
        //     TestUtils.Fail();
        // }
    }
}
