using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestTileGroup : TestDataClass<TileGroupData>
{
    public TestTileGroup() : base("tile_group"){}
    [TestMethod]
    public void LoadAndValidateTileGroup()
    {
        LoadAndValidate();
    }
}
