using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestAssetLogData  : TestDataClass<AssetLogData>
{
    public TestAssetLogData() : base("asset_log"){}
    protected override AssetLogData Mock()
    {
        return MockData.MockAssetLog();
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
