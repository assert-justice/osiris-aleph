using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionF32(float value) : PrionNode(PrionType.F32)
    {
        public float Value = value;

        public override JsonNode ToJson()
        {
            throw new System.NotImplementedException();
        }
        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
        public static new bool TryFromString(string value, out PrionNode node)
        {
            node = new PrionError("f32 string parsing not yet implemented");
            return false;
        }
        public static new bool TryFromJson(JsonNode jsonNode, out PrionNode node)
        {
            node = new PrionError("f32 json parsing not yet implemented");
            return false;
        }
    }
}
