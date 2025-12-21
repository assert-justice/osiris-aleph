using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestHandoutData : TestDataClass<HandoutData>
{
    public TestHandoutData() : base("handout"){}
    public override HandoutData Mock()
    {
        int numVisible = MockData.Rng.Next(5, 10);
        int numOwners = MockData.Rng.Next(4);
        HandoutData data = new(Guid.NewGuid())
        {
            DisplayName = MockData.GetRandomIdent(),
            ImageFilename = MockData.GetRandomIdent(),
            Text = MockData.GetRandomText(1000),
            VisibleTo = MockData.GetRandomSet(Guid.NewGuid, numVisible),
            OwnedBy = MockData.GetRandomSet(Guid.NewGuid, numVisible),
            GMNotes = MockData.GetRandomText(1000),
        };
        return data;
    }

    [TestMethod]
    public void LoadAndValidateHandout()
    {
        LoadAndValidate();
    }
    [TestMethod]
    public void MockAndValidateHandout()
    {
        MockAndValidate(100);
    }
}
