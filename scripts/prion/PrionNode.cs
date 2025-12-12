
using System.Text.Json.Nodes;

namespace Prion
{
    public abstract class PrionNode(PrionType type)
    {
        public readonly PrionType Type = type;
        public static PrionNode FromString(string value)
        {
            switch (value)
            {
                case "null":
                    return new PrionNull();
                case "true":
                    return new PrionBoolean(true);
                case "false":
                    return new PrionBoolean(false);
                default:
                    break;
            }
            if (value.StartsWith('[')){return new PrionError("Parsing arrays not yet implemented");}
            else if (value.StartsWith("bitfield:"))
            {
                PrionBitfield.TryFromString(value, out PrionNode node);
                return node;
            }
            else if (value.StartsWith("color:"))
            {
                PrionColor.TryFromString(value, out PrionNode node);
                return node;
            }
            else if (value.StartsWith('{'))
            {
                PrionDict.TryFromString(value, out PrionNode node);
                return node;
            }
            else if (value.StartsWith("dynamic:"))
            {
                PrionDynamic.TryFromString(value, out PrionNode node);
                return node;
            }
            else if (value.StartsWith("enum:"))
            {
                PrionEnum.TryFromString(value, out PrionNode node);
                return node;
            }
            else if (value.StartsWith("error:"))
            {
                PrionError.TryFromString(value, out PrionNode node);
                return node;
            }
            else if (value.StartsWith("f32:"))
            {
                PrionF32.TryFromString(value, out PrionNode node);
                return node;
            }
            else if (value.StartsWith("guid:"))
            {
                PrionGuid.TryFromString(value, out PrionNode node);
                return node;
            }
            else if (value.StartsWith("i32:"))
            {
                PrionI32.TryFromString(value, out PrionNode node);
                return node;
            }
            else if (value.StartsWith("rect2i:"))
            {
                PrionRect2I.TryFromString(value, out PrionNode node);
                return node;
            }
            else if (value.StartsWith("schema:"))
            {
                PrionSchema.TryFromString(value, out PrionNode node);
                return node;
            }
            else if (value.StartsWith("schema_file:"))
            {
                PrionSchema.TryFromString(value, out PrionNode node);
                return node;
            }
            else if (value.StartsWith("union:"))
            {
                PrionUnion.TryFromString(value, out PrionNode node);
                return node;
            }
            else if (value.StartsWith("vector2i:"))
            {
                PrionVector2I.TryFromString(value, out PrionNode node);
                return node;
            }
            return new PrionString(value);
        }
        public abstract override string ToString();
        public abstract JsonNode ToJson();
    }
}
