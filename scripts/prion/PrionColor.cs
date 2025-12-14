using System.Text;
using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionColor : PrionNode
    {
        public byte Red;
        public byte Green;
        public byte Blue;
        public byte Alpha;
        public PrionColor() : base(PrionType.Color){}
        public PrionColor(byte red, byte green, byte blue, byte alpha) : base(PrionType.Color)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }
        public PrionColor(ulong value) : base(PrionType.Color)
        {
            var bf = PrionUBigInt.FromULong(value);
            _ = new PrionColor(bf);
        }
        public PrionColor(PrionUBigInt bf) : base(PrionType.Color)
        {
            Alpha = (byte)bf.GetRange(0, 8);
            Blue = (byte)bf.GetRange(8, 8);
            Green = (byte)bf.GetRange(16, 8);
            Red = (byte)bf.GetRange(24, 8);
        }
        public override JsonNode ToJson()
        {
            throw new System.NotImplementedException();
        }
        public override string ToString()
        {
            return $"color:{ToHtmlString()}";
        }
        public static new bool TryFromString(string value, out PrionNode node)
        {
            if (!value.StartsWith("color:"))
            {
                node = new PrionError($"color signature not present at start of string {value}.");
                return false;
            }
            value = value[8..].Trim();
            if(value.StartsWith("0x") && TryFromHexString(value, out node)) return true;
            if(value.StartsWith("#") && TryFromHtmlString(value, out node)) return true;
            node = new PrionError($"Invalid color value {value}");
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
        public string ToHexString()
        {
            var val = new PrionUBigInt();
            val.PushByte(Alpha);
            val.PushByte(Blue);
            val.PushByte(Green);
            val.PushByte(Red);
            return val.ToHexString();
        }
        public string ToHtmlString()
        {
            return "#" + ToHexString()[2..];
        }
        public static bool TryFromHtmlString(string htmlString, out PrionNode node)
        {
            htmlString = htmlString.Trim();
            if (!htmlString.StartsWith('#'))
            {
                node = new PrionError($"Html strings must begin with #, found '{htmlString}'");
                return false;
            }
            htmlString = htmlString[1..];
            if(htmlString.Length != 3 && htmlString.Length != 4 && htmlString.Length != 6 && htmlString.Length != 8)
            {
                node = new PrionError($"Invalid length {htmlString.Length + 1} for html color string, found '{htmlString}'");
                return false;
            }
            return TryFromHexString("0x" + htmlString, out node);
        }
        public static bool TryFromHexString(string hexString, out PrionNode node)
        {
            if (!hexString.StartsWith("0x"))
            {
                node = new PrionError($"Hex strings must begin with 0x, found '{hexString}'");
                return false;
            }
            hexString = hexString[2..].Trim();
            var sb = new StringBuilder(10);
            sb.Append("0x");
            switch (hexString.Length)
            {
                case 3:
                    sb.Append(hexString[0]);
                    sb.Append(hexString[0]);
                    sb.Append(hexString[1]);
                    sb.Append(hexString[1]);
                    sb.Append(hexString[2]);
                    sb.Append(hexString[2]);
                    sb.Append("ff");
                    break;
                case 4:
                    sb.Append(hexString[0]);
                    sb.Append(hexString[0]);
                    sb.Append(hexString[1]);
                    sb.Append(hexString[1]);
                    sb.Append(hexString[2]);
                    sb.Append(hexString[2]);
                    sb.Append(hexString[3]);
                    sb.Append(hexString[3]);
                    break;
                case 6:
                    sb.Append(hexString);
                    sb.Append("ff");
                    break;
                case 8:
                    sb.Append(hexString);
                    break;
                default:
                    node = new PrionError($"Invalid length {hexString.Length + 2} for hex color string, found '{hexString}'");
                    return false;
            }
            if(PrionUBigInt.TryFromHexString(sb.ToString(), out PrionNode bf))
            {
                node = new PrionColor(bf as PrionUBigInt);
                return true;
            }
            node = new PrionError($"Oops");
            return false;
        }
    }
}
