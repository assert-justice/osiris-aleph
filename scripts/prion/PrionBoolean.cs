using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionBoolean(bool value) : PrionNode(PrionType.Boolean)
    {
        public readonly bool Value = value;

        public override JsonNode ToJson()
        {
            throw new System.NotImplementedException();
        }
        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
    }
}
