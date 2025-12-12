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

        public override bool TryAs<T>(out T result)
        {
            throw new System.NotImplementedException();
        }
    }
}
