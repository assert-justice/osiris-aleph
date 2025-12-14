using System;
using System.Text.Json.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prion;

namespace Osiris.Tests
{
    [TestClass]
    public class TestAssetLog
    {
        [TestMethod]
        public void Create()
        {
            OsirisFileAccess.EnterTestMode();
            var zaneUserId = Guid.NewGuid();
            var jasonUserId = Guid.NewGuid();
            var nickUserId = Guid.NewGuid();
            var log = new AssetLog();
            log.Add("niles_token.png", zaneUserId);
            log.Add("niles_portrait.png", zaneUserId);
            log.Add("dunsten_token.png", jasonUserId);
            log.Add("dunsten_portrait.png", jasonUserId);
            log.Add("dunsten_portrait.png", nickUserId);
            var snapshotString = OsirisFileAccess.ReadFile("scripts/schemas/asset_log_example.json");
            var jsonNode = JsonNode.Parse(snapshotString);
            PrionNode.TryFromJson(jsonNode, out PrionNode prionNode);
            AssetLog.TryFromNode(prionNode, out AssetLog snapshotLog);
        }
    }
}
