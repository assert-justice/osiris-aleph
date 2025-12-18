using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestHandoutData : TestDataClass<HandoutData>
{
    public TestHandoutData() : base("handout"){}

    [TestMethod]
    public void LoadAndValidateHandout()
    {
        LoadAndValidate();
    }
}
