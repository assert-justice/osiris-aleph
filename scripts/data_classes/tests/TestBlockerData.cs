using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestBlockerData : TestDataClass<BlockerData>
{
    public TestBlockerData() : base("blocker"){}

    [TestMethod]
    public void LoadAndValidateBlocker()
    {
        LoadAndValidate();
    }
    [TestMethod]
    public void MockAndValidateBlockerData()
    {
        for (int idx = 0; idx < 100; idx++)
        {
            Validate(MockClass.MockBlocker());
        }
    }
}
