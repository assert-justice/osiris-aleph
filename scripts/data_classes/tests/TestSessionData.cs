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
        for (int idx = 0; idx < 10; idx++)
        {
            Validate(MockClass.MockSession());
        }
    }
}
