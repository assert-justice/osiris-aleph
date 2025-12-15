using System;
using System.Text.Json.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prion;

namespace Osiris.Tests
{
    [TestClass]
    public class TestActor
    {
        [TestMethod]
        public void LoadAndValidateActor()
        {
            OsirisSystem.EnterTestMode();
            string snapshotString = OsirisSystem.ReadFile("scripts/schemas/actor_example.json");
            var snapshotJson = JsonNode.Parse(snapshotString);
            if(!PrionNode.TryFromJson(snapshotJson, out PrionNode prionNode))
            {
                if(prionNode is PrionError prionError)
                {
                    OsirisSystem.ReportError(string.Join("\n", prionError.Messages));
                }
                OsirisSystem.ReportError($"Unable to convert json: {snapshotJson}");
                TestUtils.Fail();
            }
            if(!SchemaManager.TryFromNode(prionNode, out Actor actor))
            {
                TestUtils.Fail();
            }
            // if(!SchemaManager.TryToNode(assetLog, out PrionNode _))
            // {
            //     TestUtils.Fail();
            // }
        }
    }
}
