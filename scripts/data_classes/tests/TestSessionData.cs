using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestSessionData : TestDataClass<SessionData>
{
    public TestSessionData() : base("session"){}
    readonly TestAssetLogData TestAssetLog = new();
    readonly TestActorData TestActor = new();
    readonly TestHandoutData TestHandout = new();
    readonly TestMapData TestMap = new();
    readonly TestUserData TestUser = new();
    public override SessionData Mock()
    {
        int numUsers = MockData.Rng.Next(20);
        int numActors = MockData.Rng.Next(20, 50);
        int numHandouts = MockData.Rng.Next(20);
        int numMaps = MockData.Rng.Next(3, 5);
        SessionData data = new()
        {
            AssetLog = TestAssetLog.Mock()
        };
        foreach (var item in MockData.GetRandomList(TestActor.Mock, numActors))
        {
            data.Actors.Add(item.Id, item);
        }
        foreach (var item in MockData.GetRandomList(TestHandout.Mock, numHandouts))
        {
            data.Handouts.Add(item.Id, item);
        }
        foreach (var item in MockData.GetRandomList(TestMap.Mock, numMaps))
        {
            data.Maps.Add(item.Id, item);
        }
        foreach (var item in MockData.GetRandomList(TestUser.Mock, numUsers))
        {
            data.Users.Add(item.Id, item);
        }
        return data;
    }
    [TestMethod]
    public void LoadAndValidateSession()
    {
        OsirisSystem.LoadAllSchemas();
        LoadAndValidate();
    }
    [TestMethod]
    public void MockAndValidateSession()
    {
        OsirisSystem.LoadAllSchemas();
        MockAndValidate(10);
    }
}
