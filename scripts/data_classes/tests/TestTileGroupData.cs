using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestTileGroupData : TestDataClass<TileGroupData>
{
    public TestTileGroupData() : base("tile_group"){}

    [TestMethod]
    public void LoadAndValidateActor()
    {
        LoadAndValidate();
    }
    [TestMethod]
    public void MockAndValidateActor()
    {
        for (int idx = 0; idx < 100; idx++)
        {
            Validate(MockClass.MockTileGroup());
        }
    }
}
