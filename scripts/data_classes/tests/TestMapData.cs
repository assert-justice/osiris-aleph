using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestMapData : TestDataClass<MapData>
{
    public TestMapData() : base("map")
    {
        AddDependency("stamp", typeof(StampData));
        AddDependency("layer", typeof(LayerData));
        AddDependency("blocker", typeof(BlockerData));
        AddDependency("tile_group", typeof(TileGroupData));
    }
    [TestMethod]
    public void LoadAndValidateMap()
    {
        LoadAndValidate();
    }
    [TestMethod]
    public void MockAndValidateMap()
    {
        for (int idx = 0; idx < 10; idx++)
        {
            Validate(MockClass.MockMap());
        }
    }
}
