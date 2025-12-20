using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestBlockerData : TestDataClass<BlockerData>
{
    public TestBlockerData() : base("blocker"){}
    public override BlockerData Mock()
    {
        BlockerData data = new();
        data.Start = MockData.GetRandomVector2I(-100, 100, -100, 100);
        data.End = MockData.GetRandomVector2I(-100, 100, -100, 100);
        data.Status = (BlockerData.BlockerStatus)MockData.Rng.Next(4);
        data.Opaque = MockData.GetRandomBool();
        data.BlocksEffects = MockData.GetRandomBool();
        return data;
    }

    [TestMethod]
    public void LoadAndValidateBlocker()
    {
        LoadAndValidate();
    }
    [TestMethod]
    public void MockAndValidateBlockerData()
    {
        MockAndValidate(100);
    }
}
