using System;
using System.Text.Json.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prion;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestActorData
{
    [TestMethod]
    public void LoadAndValidateActor()
    {
        string snapshotString = OsirisSystem.ReadFile("scripts/schemas/actor_example.json");
        var snapshotJson = JsonNode.Parse(snapshotString);
        snapshotString = snapshotJson.ToJsonString();
        if(!PrionNode.TryFromJson(snapshotJson, out PrionNode prionNode))
        {
            if(prionNode is PrionError prionError)
            {
                OsirisSystem.ReportError(string.Join("\n", prionError.Messages));
            }
            TestUtils.Fail($"Unable to convert json: {snapshotJson}");
        }
        if(!OsirisSystem.SchemaManager.Validate<ActorData>(prionNode, out string error))
        {
            TestUtils.Fail(error);
        }
        if(!IDataClass.TryFromNode(prionNode, out ActorData actor)) TestUtils.Fail();
        prionNode = actor.ToNode();
        if(!OsirisSystem.SchemaManager.Validate<ActorData>(prionNode, out error))
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
