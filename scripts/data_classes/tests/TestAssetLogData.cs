using System;
using System.Text.Json.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prion;

namespace Osiris.Tests
{
    [TestClass]
    public class TestAssetLogData
    {
        // [TestMethod]
        // public void Snapshot()
        // {
        //     OsirisSystem.EnterTestMode();
        //     Guid zaneUserId = Guid.NewGuid();
        //     Guid jasonUserId = Guid.NewGuid();
        //     Guid nickUserId = Guid.NewGuid();
        //     AssetLog log = new();
        //     log.Add("niles_token.png", zaneUserId);
        //     log.Add("niles_portrait.png", zaneUserId);
        //     log.Add("dunsten_token.png", jasonUserId);
        //     log.Add("dunsten_portrait.png", jasonUserId);
        //     log.Add("dunsten_portrait.png", nickUserId);
        //     PrionNode prionNode = log.ToNode();
        //     var jsonNode = prionNode.ToJson();
        //     string snapshotString = OsirisSystem.ReadFile("scripts/schemas/asset_log_example.json");
        //     var snapshotJson = JsonNode.Parse(snapshotString);
        //     if(jsonNode.ToJsonString() != snapshotJson.ToJsonString())
        //     {
        //         OsirisSystem.ReportError("Generated json:");
        //         OsirisSystem.ReportError(jsonNode.ToJsonString());
        //         OsirisSystem.ReportError("Snapshot json:");
        //         OsirisSystem.ReportError(snapshotJson.ToJsonString());
        //         Assert.Fail(OsirisSystem.GetErrors());
        //     }
        // }
        [TestMethod]
        public void LoadAndValidate()
        {
            OsirisSystem.EnterTestMode();
            string snapshotString = OsirisSystem.ReadFile("scripts/schemas/asset_log_example.json");
            var snapshotJson = JsonNode.Parse(snapshotString);
            Assert.IsTrue(PrionNode.TryFromJson(snapshotJson, out PrionNode prionNode));
            if(!SchemaManager.TryFromNode(prionNode, out AssetLogData assetLog))
            {
                Assert.Fail(OsirisSystem.GetErrors());
            }
            if(assetLog is null)
            {
                Assert.Fail("Asset log was null");
            }
            if(!SchemaManager.TryToNode(assetLog, out PrionNode _))
            {
                Assert.Fail(OsirisSystem.GetErrors());
            }
        }
    }
}
