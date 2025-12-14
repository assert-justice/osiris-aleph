using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionRect2I(PrionVector2I position, PrionVector2I size) : PrionNode(PrionType.Rect2I)
    {
        public PrionVector2I Position = position;
        public PrionVector2I Size = size;

        public override JsonNode ToJson()
        {
            return JsonNode.Parse($"\"{ToString()}\"");
        }
        public override string ToString()
        {
            return $"rect2i:{Position.X},{Position.Y},{Size.X},{Size.Y}";
        }
        public static new bool TryFromString(string value, out PrionNode node)
        {
            value = value.Trim();
            if (!value.StartsWith("rect2i:"))
            {
                node = new PrionError($"rect2i signature not present at start of string '{value}'.");
                return false;
            }
            value = value[9..];
            string[] coords = value.Split(',');
            if(coords.Length != 4)
            {
                node = new PrionError($"rect2i expects exactly four comma separated integers after the signature.");
                return false;
            }
            List<int> ints = [];
            foreach (var item in coords)
            {
                if(!int.TryParse(item, out int v))
                {
                    node = new PrionError($"could not parse '{coords[0]}' as an integer.");
                    return false;
                }
                ints.Add(v);
            }
            node = new PrionRect2I(new(ints[0],ints[1]), new(ints[2],ints[3]));
            return true;
        }
        public static new bool TryFromJson(JsonNode jsonNode, out PrionNode node)
        {
            if(jsonNode is null)
            {
                node = new PrionError("Invalid json kind. Value cannot be null.");
                return false;
            }
            var kind = jsonNode.GetValueKind();
            if(kind == System.Text.Json.JsonValueKind.String)
            {
                if(jsonNode.AsValue().TryGetValue(out string sValue))
                {
                    return TryFromString(sValue, out node);
                }
                node = new PrionError("Should be unreachable");
                return false;
            }
            else
            {
                node = new PrionError("Invalid json kind");
                return false;
            }
        }
    }
}
