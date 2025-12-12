using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionDict : PrionNode
    {
        public Dictionary<string, PrionNode> Dict;
        public PrionDict() : base(PrionType.Dict)
        {
            Dict = [];
        }
        public PrionDict(Dictionary<string, PrionNode> dict) : base(PrionType.Dict)
        {
            Dict = dict;
        }
        public override JsonNode ToJson()
        {
            throw new System.NotImplementedException();
        }
        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
        public static bool TryFromString(string value, out PrionNode node)
        {
            node = new PrionError("bitfield parsing not yet implemented");
            return false;
        }

    }
}
