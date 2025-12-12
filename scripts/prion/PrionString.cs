using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionString : PrionNode
    {
        public readonly string Text;
        public PrionString() : base(PrionType.String)
        {
            Text = "";
        }
        public PrionString(string text) : base(PrionType.String)
        {
            Text = text;
        }
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
