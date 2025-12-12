using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionGuid : PrionNode
    {
        public PrionGuid() : base(PrionType.Guid){}

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
            node = new PrionError("guid parsing not yet implemented");
            return false;
        }
    }
}
