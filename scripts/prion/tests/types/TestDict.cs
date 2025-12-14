using System.Text.Json.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Prion.Tests
{
    [TestClass]
    public class TestDict
    {
        [TestMethod]
        public void Create()
        {
            var jsonNode = JsonNode.Parse("{\"bite me\": \"poo\"}");
            if(jsonNode.GetValueKind() != System.Text.Json.JsonValueKind.Object)
            {
                Assert.Fail($"Expected json object, found '{jsonNode.GetValueKind()}'");
            }
            PrionNode.TryFromJson(jsonNode, out PrionNode prionNode);
            if(prionNode.Type != PrionType.Dict)
            {
                Assert.Fail($"Expected dict, found '{prionNode.Type}'");
            }
        }
    }
}
