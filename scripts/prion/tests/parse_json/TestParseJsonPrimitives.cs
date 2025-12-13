using System.Text.Json.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Prion.Tests
{
    [TestClass]
    public class TestParseJsonPrimitive
    {
        [TestMethod]
        public void ParseNull()
        {
            var jsonNode = JsonNode.Parse("null");
            Assert.IsTrue(PrionNode.TryFromJson(jsonNode, out PrionNode prionNode));
            Assert.IsTrue(prionNode is PrionNull);
        }
        [TestMethod]
        public void ParseString()
        {
            var jsonNode = JsonNode.Parse("\"test\"");
            Assert.IsTrue(PrionNode.TryFromJson(jsonNode, out PrionNode prionNode));
            Assert.IsTrue(prionNode is PrionString);
        }
        [TestMethod]
        public void ParseNumber()
        {
            string str = "10";
            var jsonNode = JsonNode.Parse(str);
            Assert.IsTrue(PrionNode.TryFromJson(jsonNode, out PrionNode prionNode));
            Assert.IsTrue(prionNode is PrionF32);
        }
        [TestMethod]
        public void ParseBool()
        {
            var a = JsonNode.Parse("true");
            var b = JsonNode.Parse("false");
            Assert.IsTrue(PrionNode.TryFromJson(a, out PrionNode a2));
            Assert.IsTrue(PrionNode.TryFromJson(b, out PrionNode b2));
            Assert.IsTrue(a2 is PrionBoolean);
            Assert.IsTrue(b2 is PrionBoolean);
        }
    }
}
