using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestAssetLogData  : TestDataClass<AssetLogData>
{
    public TestAssetLogData() : base("asset_log"){}
    public override AssetLogData Mock()
    {
        int numOwners = MockData.Rng.Next(5, 10);
        List<Guid> owners = new(numOwners);
        for (int idx = 0; idx < numOwners; idx++)
        {
            owners.Add(Guid.NewGuid());
        }
        int numFiles = MockData.Rng.Next(10, 20);
        List<string> files = new(numFiles);
        for (int idx = 0; idx < numFiles; idx++)
        {
            files.Add(MockData.GetRandomIdent());
        }
        AssetLogData data = new();
        foreach (var file in files)
        {
            int numFileOwners = MockData.Rng.Next(1, numOwners);
            for (int idx = 0; idx < numFileOwners; idx++)
            {
                data.Add(file, MockData.GetRandomElement(owners));
            }
        }
        return data;
    }

    [TestMethod]
    public void LoadAndValidateAssetLog()
    {
        LoadAndValidate();
    }
    [TestMethod]
    public void MockAndValidateAssetLog()
    {
        MockAndValidate(100);
    }
}
