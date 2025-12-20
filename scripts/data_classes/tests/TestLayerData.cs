using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestLayerData : TestDataClass<LayerData>
{
    public TestLayerData() : base("layer"){}
    [TestMethod]
    public void LoadAndValidateLayer()
    {
        AddDependency("stamp", typeof(StampData));
        LoadAndValidate();
    }
}
