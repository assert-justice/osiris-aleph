using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestLayerData : TestDataClass<LayerData>
{
    public TestLayerData() : base("layer")
    {
        AddDependency("stamp", typeof(StampData));
    }
    [TestMethod]
    public void LoadAndValidateLayer()
    {
        LoadAndValidate();
    }
}
