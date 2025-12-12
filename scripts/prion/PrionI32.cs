using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionI32 : PrionNode
    {
        public PrionI32() : base(PrionType.I32){}

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
            node = new PrionError("enum parsing not yet implemented");
            return false;
        }
    }
}
