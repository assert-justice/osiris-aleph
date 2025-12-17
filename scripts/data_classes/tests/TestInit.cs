using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Osiris.Tests;

[TestClass]
public class TestInit
{
    [AssemblyInitialize]
    public static void Init(TestContext _)
    {
        OsirisSystem.EnterTestMode();
        OsirisSystem.LoadSchemas();
    }
}
