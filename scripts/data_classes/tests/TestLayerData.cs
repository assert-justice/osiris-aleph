using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestLayerData : TestDataClass<LayerData>
{
    public TestLayerData() : base("layer")
    {
        AddDependency("stamp", typeof(StampData));
    }
    public override LayerData Mock()
    {
        int numStamps = MockData.Rng.Next(20);
        LayerData data = new()
        {
            DisplayName = MockData.GetRandomIdent(),
            IsVisible = MockData.GetRandomBool(),
            // Stamps = MockData.GetRandomList(()=>TestStampData.Moc, numStamps),
        };
        return data;
    }
    [TestMethod]
    public void LoadAndValidateLayer()
    {
        LoadAndValidate();
    }
}
