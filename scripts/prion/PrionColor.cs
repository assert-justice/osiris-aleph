using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionColor : PrionNode
    {
        public PrionColor() : base(PrionType.Color){}

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
            node = new PrionError("color parsing not yet implemented");
            return false;
        }
    }
}
