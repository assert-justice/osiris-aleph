using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionBoolean(bool value) : PrionNode(PrionType.Boolean)
    {
        public readonly bool Value = value;

        public override JsonNode ToJson()
        {
            return JsonNode.Parse(ToString());
        }
        public override string ToString()
        {
            return Value ? "true" : "false";
        }
    }
}
