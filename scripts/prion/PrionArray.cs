using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionArray : PrionNode
    {
        public List<PrionNode> Array;
        public PrionArray() : base(PrionType.Array){}

        public override JsonNode ToJson()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
        public static new bool TryFromString(string value, out PrionNode node)
        {
            node = new PrionError("array string parsing not yet implemented");
            return false;
        }
        public static new bool TryFromJson(JsonNode jsonNode, out PrionNode node)
        {
            node = new PrionError("array json parsing not yet implemented");
            return false;
        }
    }
}
