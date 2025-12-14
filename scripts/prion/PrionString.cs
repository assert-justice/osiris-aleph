using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionString : PrionNode
    {
        public readonly string Text = "";
        public PrionString() : base(PrionType.String){}
        public PrionString(string text) : base(PrionType.String)
        {
            Text = text;
        }
        public override JsonNode ToJson()
        {
            return JsonNode.Parse($"\"{Text}\"");
        }
        public override string ToString()
        {
            return Text;
        }
        public static PrionString FromString(string text)
        {
            return new(text);
        }
    }
}
