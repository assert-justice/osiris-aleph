using System.Text.Json.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Prion.Tests
{
    [TestClass]
    public class TestParseJsonNumber
    {
        [TestMethod]
        public void ParseJsonNumber()
        {
            var jsonNode = JsonNode.Parse("10");
            Assert.IsTrue(PrionNode.TryFromJson(jsonNode, out PrionNode prionNode));
            Assert.IsTrue(prionNode is PrionF32);
            Assert.IsTrue(PrionF32.TryFromJson(jsonNode, out prionNode));
            Assert.IsTrue(prionNode is PrionF32);
        }
        [TestMethod]
        public void ParseJsonF32()
        {
            string str = "f32:10";
            string jsonStr = $"\"{str}\"";
            var jsonNode = JsonNode.Parse(jsonStr);
            Assert.IsTrue(PrionNode.TryFromJson(jsonNode, out PrionNode prionNode));
            Assert.IsTrue(prionNode is PrionF32);
            Assert.IsTrue(prionNode.ToString() == str);
            Assert.IsTrue(PrionF32.TryFromJson(jsonNode, out prionNode));
            Assert.IsTrue(prionNode is PrionF32);
            Assert.IsTrue(prionNode.ToString() == str);
            Assert.IsTrue(prionNode.ToJson().ToString() == str);
            jsonNode = JsonNode.Parse("\"10\"");
            Assert.IsFalse(PrionF32.TryFromJson(jsonNode, out prionNode));
            Assert.IsTrue(prionNode is PrionError);
        }
        [TestMethod]
        public void ParseJsonI32()
        {
            string str = "i32:10";
            string jsonStr = $"\"{str}\"";
            var jsonNode = JsonNode.Parse(jsonStr);
            Assert.IsTrue(PrionNode.TryFromJson(jsonNode, out PrionNode prionNode));
            Assert.IsTrue(prionNode is PrionI32);
            Assert.IsTrue(PrionI32.TryFromJson(jsonNode, out prionNode));
            Assert.IsTrue(prionNode is PrionI32);
            Assert.IsTrue(prionNode.ToString() == str);
            Assert.IsTrue(prionNode.ToJson().ToString() == str);
            jsonNode = JsonNode.Parse("\"10\"");
            Assert.IsFalse(PrionI32.TryFromJson(jsonNode, out prionNode));
            Assert.IsTrue(prionNode is PrionError);
        }
        [TestMethod]
        public void ParseJsonUBigInt()
        {
            string str = "ubigint:0x10";
            string jsonStr = $"\"{str}\"";
            var jsonNode = JsonNode.Parse(jsonStr);
            Assert.IsTrue(PrionNode.TryFromJson(jsonNode, out PrionNode prionNode));
            Assert.IsTrue(prionNode is PrionUBigInt);
            Assert.IsTrue(PrionUBigInt.TryFromJson(jsonNode, out prionNode));
            Assert.IsTrue(prionNode is PrionUBigInt);
            Assert.IsTrue(prionNode.ToString() == str);
            Assert.IsTrue(prionNode.ToJson().ToString() == str);
            jsonNode = JsonNode.Parse("\"10\"");
            Assert.IsFalse(PrionUBigInt.TryFromJson(jsonNode, out prionNode));
            Assert.IsTrue(prionNode is PrionError);
        }
    }
}
