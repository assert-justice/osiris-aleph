using System.Text.Json.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Prion.Tests
{
    [TestClass]
    public class TestSchema
    {
        [TestMethod]
        public void Create()
        {
            string exampleSchema = TestUtils.ReadFile("scripts/schemas/actor_schema.json");
            var jsonNode = JsonNode.Parse(exampleSchema);
            if(jsonNode.GetValueKind() != System.Text.Json.JsonValueKind.Object)
            {
                Assert.Fail($"Expected json object, found '{jsonNode.GetValueKind()}'");
            }
            PrionNode.TryFromJson(jsonNode, out PrionNode prionNode);
            // Assert.Fail(exampleSchema);
            PrionSchema.TryFromNode(prionNode, out PrionSchema _, out string error);
            if(error != "")
            {
                Assert.Fail(error);
            }
        }
    }
}
