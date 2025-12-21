using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestUserData : TestDataClass<UserData>
{
    public TestUserData() : base("user"){}

    [TestMethod]
    public void LoadAndValidateUser()
    {
        LoadAndValidate();
    }
    [TestMethod]
    public void MockAndValidateUser()
    {
        for (int idx = 0; idx < 100; idx++)
        {
            Validate(MockClass.MockUser());
        }
    }
}