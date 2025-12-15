using System;
using System.Text.Json.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prion;

namespace Osiris.Tests
{
    [TestClass]
    public class TestHandoutData
    {
        [TestMethod]
        public void LoadAndValidateHandout()
        {
            OsirisSystem.EnterTestMode();
            string snapshotString = OsirisSystem.ReadFile("scripts/schemas/handout_example.json");
            var snapshotJson = JsonNode.Parse(snapshotString);
            snapshotString = snapshotJson.ToJsonString();
            if(!PrionNode.TryFromJson(snapshotJson, out PrionNode prionNode))
            {
                if(prionNode is PrionError prionError)
                {
                    OsirisSystem.ReportError(string.Join("\n", prionError.Messages));
                }
                OsirisSystem.ReportError($"Unable to convert json: {snapshotJson}");
                TestUtils.Fail();
            }
            if(!SchemaManager.TryFromNode(prionNode, out HandoutData handout))
            {
                TestUtils.Fail();
            }
            if(!SchemaManager.TryToNode(handout, out PrionNode node))
            {
                TestUtils.Fail();
            }
            if(node.ToJson().ToJsonString() != snapshotString)
            {
                OsirisSystem.ReportError(node.ToJson().ToJsonString());
                OsirisSystem.ReportError(snapshotString);
                TestUtils.Fail();
            }
        }
    }
}
