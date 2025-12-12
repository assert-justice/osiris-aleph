
using System.Text.Json.Nodes;

namespace Prion
{
    public abstract class PrionNode(PrionType type)
    {
        public readonly PrionType Type = type;
        public static bool TryFromString(string value, out PrionNode node)
        {
            switch (value)
            {
                case "null":
                    node = new PrionNull();
                    return true;
                case "true":
                    node = new PrionBoolean(true);
                    return true;
                case "false":
                    node = new PrionBoolean(false);
                    return true;
                default:
                    break;
            }
            if (value.StartsWith('[')){
                return PrionArray.TryFromString(value, out node);
                // prionNode = new PrionError("Parsing arrays not yet implemented");
                // return false;
            }
            else if (value.StartsWith("bitfield:"))
            {
                return PrionBitfield.TryFromString(value, out node);
            }
            else if (value.StartsWith("color:"))
            {
                return PrionColor.TryFromString(value, out node);
            }
            else if (value.StartsWith('{'))
            {
                return PrionDict.TryFromString(value, out node);
            }
            else if (value.StartsWith("dynamic:"))
            {
                return PrionDynamic.TryFromString(value, out node);
            }
            else if (value.StartsWith("enum:"))
            {
                return PrionEnum.TryFromString(value, out node);
            }
            else if (value.StartsWith("error:"))
            {
                node = new PrionError(value[6..].Trim());
                return true;
            }
            else if (value.StartsWith("f32:"))
            {
                return PrionF32.TryFromString(value, out node);
            }
            else if (value.StartsWith("guid:"))
            {
                return PrionGuid.TryFromString(value, out node);
            }
            else if (value.StartsWith("i32:"))
            {
                return PrionI32.TryFromString(value, out node);
            }
            else if (value.StartsWith("rect2i:"))
            {
                return PrionRect2I.TryFromString(value, out node);
            }
            else if (value.StartsWith("schema:"))
            {
                return PrionSchema.TryFromString(value, out node);
            }
            else if (value.StartsWith("schema_file:"))
            {
                return PrionSchema.TryFromString(value, out node);
            }
            // else if (value.StartsWith("union:"))
            // {
            //     return PrionUnion.TryFromString(value, out node);
            // }
            else if (value.StartsWith("vector2i:"))
            {
                return PrionVector2I.TryFromString(value, out node);
            }
            node = new PrionString(value);
            return true;
        }
        public static bool TryFromJson(JsonNode jsonNode, out PrionNode prionNode)
        {
            var kind = jsonNode.GetValueKind();
            switch (kind)
            {
                case System.Text.Json.JsonValueKind.Undefined:
                case System.Text.Json.JsonValueKind.Null:
                    prionNode = new PrionNull();
                    return true;
                case System.Text.Json.JsonValueKind.True:
                case System.Text.Json.JsonValueKind.False:
                    prionNode = new PrionBoolean(kind == System.Text.Json.JsonValueKind.True);
                    return true;
                case System.Text.Json.JsonValueKind.Array:
                    return PrionArray.TryFromJson(jsonNode, out prionNode);
                case System.Text.Json.JsonValueKind.Number:
                    return PrionF32.TryFromJson(jsonNode, out prionNode);
                case System.Text.Json.JsonValueKind.Object:
                    return PrionDict.TryFromJson(jsonNode, out prionNode);
                case System.Text.Json.JsonValueKind.String:
                    if(!jsonNode.AsValue().TryGetValue(out string str))
                    {
                        // should be unreachable.
                        prionNode = new PrionError("Oof");
                        return false;
                    }
                    return TryFromString(str, out prionNode);
                default:
                    prionNode = new PrionError("Failed to match Json Node kind");
                    return false;
            }
        }
        public abstract override string ToString();
        public abstract JsonNode ToJson();
    }
}
