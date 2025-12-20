using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestActorData : TestDataClass<ActorData>
{
    public TestActorData() : base("actor"){}
    public override ActorData Mock()
    {
        ActorData data = new(Guid.NewGuid())
        {
            DisplayName = MockData.GetRandomIdent(),
            PortraitFilename = MockData.GetRandomIdent(),
            TokenFilename = MockData.GetRandomIdent(),
            Description = MockData.GetRandomIdent()
        };
        int numOwners = MockData.Rng.Next(0, 3);
        for (int idx = 0; idx < numOwners; idx++)
        {
            data.ControlledBy.Add(Guid.NewGuid());
        }
        return data;
    }

    [TestMethod]
    public void LoadAndValidateActor()
    {
        LoadAndValidate();
    }
    [TestMethod]
    public void MockAndValidateActor()
    {
        MockAndValidate(100);
    }
}
