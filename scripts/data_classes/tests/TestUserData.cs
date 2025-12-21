using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.DataClass.Tests;

[TestClass]
public class TestUserData : TestDataClass<UserData>
{
    public TestUserData() : base("user"){}
    public override UserData Mock()
    {
        UserData data = new(Guid.NewGuid())
        {
            DisplayName = MockData.GetRandomIdent(),
            PfpFilename = MockData.GetRandomIdent()
        };
        return data;
    }

    [TestMethod]
    public void LoadAndValidateUser()
    {
        LoadAndValidate();
    }
    [TestMethod]
    public void MockAndValidateUser()
    {
        MockAndValidate(100);
    }
}