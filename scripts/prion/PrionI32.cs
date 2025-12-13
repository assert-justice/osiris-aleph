using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionI32(int value) : PrionNode(PrionType.I32)
    {
        public int Value = value;

        public override JsonNode ToJson()
        {
            return JsonNode.Parse($"\"{ToString()}\"");
        }
        public override string ToString()
        {
            return $"i32:{Value}";
        }
        public static new bool TryFromString(string value, out PrionNode node)
        {
            if (!value.StartsWith("i32:"))
            {
                node = new PrionError($"i32 signature not present at start of string {value}.");
                return false;
            }
            if(int.TryParse(value[4..], out int i))
            {
                node = new PrionI32(i);
                return true;
            }
            node = new PrionError($"could not parse string {value} as i32.");
            return false;
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
