using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestAssetLogData  : TestDataClass<AssetLogData>
{
    public TestAssetLogData() : base("asset_log"){}

    [TestMethod]
    public void LoadAndValidateAssetLog()
    {
        LoadAndValidate();
        // string snapshotString = OsirisSystem.ReadFile("scripts/schemas/asset_log_example.json");
        // var snapshotJson = JsonNode.Parse(snapshotString);
        // Assert.IsTrue(PrionNode.TryFromJson(snapshotJson, out PrionNode prionNode));
        // if(!OsirisSystem.SchemaManager.Validate<AssetLogData>(prionNode, out string error))
        // {
        //     TestUtils.Fail(error);
        // }
        // if(!IDataClass.TryFromNode(prionNode, out AssetLogData assetLog)) TestUtils.Fail();
        // prionNode = assetLog.ToNode();
        // if(!OsirisSystem.SchemaManager.Validate<AssetLogData>(prionNode, out error))
        // {
        //     TestUtils.Fail(error);
        // }
    }
}
