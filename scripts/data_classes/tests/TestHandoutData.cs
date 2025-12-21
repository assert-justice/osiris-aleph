using System;
using System.Collections.Generic;
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
    [TestMethod]
    public void MockAndValidateHandout()
    {
        for (int idx = 0; idx < 100; idx++)
        {
            Validate(MockClass.MockHandout());
        }
    }
}
