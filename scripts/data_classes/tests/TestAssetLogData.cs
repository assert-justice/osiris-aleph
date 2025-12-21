using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestAssetLogData  : TestDataClass<AssetLogData>
{
    public TestAssetLogData() : base("asset_log"){}

    [TestMethod]
    public void LoadAndValidateAssetLog()
    {
        LoadAndValidate();
    }
    [TestMethod]
    public void MockAndValidateAssetLog()
    {
        for (int idx = 0; idx < 100; idx++)
        {
            Validate(MockClass.MockAssetLog());
        }
    }
}
