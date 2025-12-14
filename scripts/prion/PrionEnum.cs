using System;
using System.Linq;
using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionEnum: PrionNode
    {
        public readonly string[] Options;
        int Index = 0;
        PrionEnum(string[] options, int index): base((PrionType.Enum))
        {
            Options = options;
            Index = index;
        }
        public override JsonNode ToJson()
        {
            return JsonNode.Parse($"\"{ToString()}\"");
        }
        public override string ToString()
        {
            string res = string.Join(", ", Options);
            return "enum: " + res + ": " + Options[Index];
        }
        public static new bool TryFromString(string value, out PrionNode node)
        {
            value = value.Trim();
            var sections = value.Split(':').Select(s => s.Trim()).ToArray();
            if(sections.Length != 3)
            {
                node = new PrionError($"Could not parse enum.");
                return false;
            }
            if (sections[0] != "enum")
            {
                node = new PrionError($"enum signature not present at start of string '{value}'.");
                return false;
            }
            var options = sections[1].Split(',').Select(s => s.Trim()).ToArray();
            foreach (var option in options)
            {
                foreach (char c in option)
                {
                    if(!PrionParseUtils.IsAlphanumericOrUnderscore(c))
                    {
                        node = new PrionError($"unexpected character '{c}' in enum option '{option}'. only alphanumeric characters or underscores are allowed.");
                        return false;
                    }
                }
            }
            string selected = sections[2];
            int index = Array.FindIndex(options, s => s == selected);
            if (index == -1)
            {
                node = new PrionError($"enum options '{options}' do not contain selected option '{selected}'.");
                return false;
            }
            node = new PrionEnum(options, index);
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
