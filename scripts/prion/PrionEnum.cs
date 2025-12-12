using System.Linq;
using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionEnum(string[] options) : PrionNode(PrionType.Enum)
    {
        public readonly string[] Options = options;
        int Index = 0;
        public override JsonNode ToJson()
        {
            throw new System.NotImplementedException();
        }
        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
        public static bool TryFromString(string value, out PrionNode node)
        {
            node = new PrionError("enum parsing not yet implemented");
            return false;
        }
        public int GetIndex()
        {
            return Index;
        }
        public string GetValue()
        {
            return Options[Index];
        }
        public bool TrySetIndex(int index)
        {
            if(index < 0 || index >= Options.Length) return false;
            Index = index;
            return true;
        }
        public bool TrySetValue(string value)
        {
            int index = Options.ToList().FindIndex(s => s == value);
            if(index == -1) return false;
            Index = index;
            return true;
        }
    }
}
