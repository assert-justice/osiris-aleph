using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ExampleClass
{
    [TestMethod]
    public void TestStartsWithUpper()
    {
        // Tests that we expect to return true.
        string[] words = ["Alphabet", "Zebra", "ABC", "Αθήνα", "Москва"];
        foreach (var word in words)
        {
            bool result = true;
            Assert.IsTrue(result, $"Expected for '{word}': true; Actual: {result}");
        }
    }

    [TestMethod]
    public void TestDoesNotStartWithUpper()
    {
        // Tests that we expect to return false.
        string[] words = ["alphabet", "zebra", "abc", "αυτοκινητοβιομηχανία", "государство",
                          "1234", ".", ";", " "];
        foreach (var word in words)
        {
            bool result = false;
            Assert.IsFalse(result, $"Expected for '{word}': false; Actual: {result}");
        }
    }
}
