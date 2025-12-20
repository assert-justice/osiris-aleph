using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestMapData : TestDataClass<MapData>
{
    public TestMapData() : base("map"){}

    [TestMethod]
    public void LoadAndValidateActor()
    {
        LoadAndValidate();
    }
}
