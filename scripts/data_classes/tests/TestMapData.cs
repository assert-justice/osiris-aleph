using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestMapData : TestDataClass<MapData>
{
    public TestMapData() : base("map"){}
    public override MapData Mock()
    {
        int numBlockers = MockData.Rng.Next(100, 200);
        int numTileGroups = MockData.Rng.Next(50);
        int numLayers = MockData.Rng.Next(3, 16);
        // Thanks, I hate it
        TestBlockerData testBlocker = new();
        TestTileGroupData testTile = new();
        TestLayerData testLayer = new();
        MapData data = new(Guid.NewGuid())
        {
            DisplayName = MockData.GetRandomIdent(),
            Size = MockData.GetRandomVector2I(0, 100, 0, 100),
            CellWidth = MockData.GetRandomFloat(32, 256),
            UsersPresent = MockData.GetRandomSet(Guid.NewGuid, MockData.Rng.Next(16)),
            LightingEnabled = MockData.GetRandomBool(),
            BackgroundColor = MockData.GetRandomColor(),
            BorderColor = MockData.GetRandomColor(),
            GridVisible = MockData.GetRandomBool(),
            GridColor = MockData.GetRandomColor(),
            GridLineWidth = MockData.GetRandomFloat(1, 16),
            Blockers = MockData.GetRandomList(testBlocker.Mock, numBlockers),
            TileGroups = MockData.GetRandomList(testTile.Mock, numBlockers),
            Layers = MockData.GetRandomList(testLayer.Mock, numBlockers),
        };
        return data;
    }

    [TestMethod]
    public void LoadAndValidateActor()
    {
        LoadAndValidate();
    }
}
