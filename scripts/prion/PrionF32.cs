using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionF32(float value) : PrionNode(PrionType.F32)
    {
        public float Value = value;

        public override JsonNode ToJson()
        {
            return JsonNode.Parse($"\"{ToString()}\"");
        }
        public override string ToString()
        {
            return $"f32:{Value}";
        }
        public static new bool TryFromString(string value, out PrionNode node)
        {
            if (!value.StartsWith("f32:"))
            {
                node = new PrionError($"f32 signature not present at start of string {value}.");
                return false;
            }
            if(float.TryParse(value[4..], out float f))
            {
                node = new PrionF32(f);
                return true;
            }
            node = new PrionError($"could not parse string {value} as f32.");
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
            switch (kind)
            {
                case System.Text.Json.JsonValueKind.Number:
                    if(jsonNode.AsValue().TryGetValue(out float fValue))
                    {
                        node = new PrionF32(fValue);
                        return true;
                    }
                    node = new PrionError("Should be unreachable");
                    return false;
                case System.Text.Json.JsonValueKind.String:
                    if(jsonNode.AsValue().TryGetValue(out string sValue))
                    {
                        return TryFromString(sValue, out node);
                    }
                    node = new PrionError("Should be unreachable");
                    return false;
                default:
                    node = new PrionError("Invalid json kind");
                    return false;
            }
        }
    }
}
