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
        MockAndValidate(()=>MockData.MockAssetLog(), 100);
    }
}
