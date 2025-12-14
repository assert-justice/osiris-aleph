using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionVector2I(int x = 0, int y = 0) : PrionNode(PrionType.Vector2I)
    {
        public int X = x;
        public int Y = y;

        public override JsonNode ToJson()
        {
            return JsonNode.Parse($"\"{ToString()}\"");
        }
        public override string ToString()
        {
            return $"vector2i:{X},{Y}";
        }
        public static new bool TryFromString(string value, out PrionNode node)
        {
            value = value.Trim();
            if (!value.StartsWith("vector2i:"))
            {
                node = new PrionError($"vector2i signature not present at start of string '{value}'.");
                return false;
            }
            value = value[9..];
            string[] coords = value.Split(',');
            if(coords.Length != 2)
            {
                node = new PrionError($"vector2i expects exactly two comma separated integers after the signature.");
                return false;
            }
            if(!int.TryParse(coords[0], out int x))
            {
                node = new PrionError($"could not parse '{coords[0]}' as an integer.");
                return false;
            }
            if(!int.TryParse(coords[0], out int y))
            {
                node = new PrionError($"could not parse '{coords[1]}' as an integer.");
                return false;
            }
            node = new PrionVector2I(x, y);
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
