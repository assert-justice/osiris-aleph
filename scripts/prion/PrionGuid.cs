using System;
using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionGuid(Guid value) : PrionNode(PrionType.Guid)
    {
        public readonly Guid Value = value;

        public override JsonNode ToJson()
        {
            return JsonNode.Parse($"\"guid:{ToString()}\"");
        }

        public override string ToString()
        {
            return Value.ToString();
        }
        public static new bool TryFromString(string value, out PrionNode node)
        {
            value = value.Trim();
            if (!value.StartsWith("guid:"))
            {
                node = new PrionError($"guid signature not present at start of string '{value}'.");
                return false;
            }
            value = value[5..].Trim();
            if(!Guid.TryParse(value, out Guid result))
            {
                node = new PrionError($"Could not parse string '{value}' as a Guid.");
                return false;
            }
            node = new PrionGuid(result);
            return true;
        }
        public static PrionGuid FromGuid(Guid guid)
        {
            return new(guid);
        }
    }
}
