
using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionNull : PrionNode
    {
        public PrionNull() : base(PrionType.Null){}

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
