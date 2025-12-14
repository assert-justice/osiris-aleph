// using System.Text.Json.Nodes;
// using Microsoft.VisualStudio.TestTools.UnitTesting;

// namespace Prion.Tests
// {
//     [TestClass]
//     public class TestParseJsonColor
//     {
//         [TestMethod]
//         public void ParseJsonColor()
//         {
//             string str = "color:#ffffff";
//             string jsonStr = $"\"{str}\"";
//             var jsonNode = JsonNode.Parse(jsonStr);
//             Assert.IsTrue(PrionNode.TryFromJson(jsonNode, out PrionNode prionNode));
//             Assert.IsTrue(prionNode is PrionColor);
//             Assert.IsTrue(PrionColor.TryFromJson(jsonNode, out prionNode));
//             Assert.IsTrue(prionNode is PrionF32);
//         }
//     }
// }
