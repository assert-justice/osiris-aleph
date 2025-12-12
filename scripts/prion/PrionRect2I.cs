using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionRect2I : PrionNode
    {
        public PrionRect2I() : base(PrionType.Rect2I){}

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
            node = new PrionError("enum parsing not yet implemented");
            return false;
        }
    }
}
