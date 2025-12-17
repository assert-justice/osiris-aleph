using System;
using System.Text.Json.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prion;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestHandoutData
{
    [TestMethod]
    public void LoadAndValidateHandout()
    {
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
        if(!OsirisSystem.SchemaManager.Validate<HandoutData>(prionNode, out string error))
        {
            TestUtils.Fail(error);
        }
        if(!IDataClass.TryFromNode(prionNode, out HandoutData handout)) TestUtils.Fail();
        prionNode = handout.ToNode();
        if(!OsirisSystem.SchemaManager.Validate<HandoutData>(prionNode, out error))
        {
            TestUtils.Fail(error);
        }
        if(prionNode.ToJson().ToJsonString() != snapshotString)
        {
            OsirisSystem.ReportError(prionNode.ToJson().ToJsonString());
            OsirisSystem.ReportError(snapshotString);
            TestUtils.Fail();
        }
    }
}
