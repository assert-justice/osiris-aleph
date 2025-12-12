using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionBitfield : PrionNode
    {
        public PrionBitfield() : base(PrionType.Bitfield){}

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
