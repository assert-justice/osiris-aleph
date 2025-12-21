using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestTileGroupData : TestDataClass<TileGroupData>
{
    public TestTileGroupData() : base("tile_group"){}
    public override TileGroupData Mock()
    {
        int numTiles = MockData.Rng.Next(200);
        TileGroupData data = new();
        data.DisplayName = MockData.GetRandomIdent();
        // TODO: test bitfield somehow.
        data.Tiles = MockData.GetRandomList(()=>MockData.GetRandomVector2I(-100, 100, -100, 100), numTiles);
        return data;
    }

    [TestMethod]
    public void LoadAndValidateActor()
    {
        LoadAndValidate();
    }
    [TestMethod]
    public void MockAndValidateActor()
    {
        MockAndValidate(100);
    }
}
