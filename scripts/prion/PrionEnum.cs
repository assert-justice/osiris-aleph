using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionEnum: PrionNode
    {
        public readonly HashSet<string> Options;
        string Value;
        PrionEnum(HashSet<string> options, string value): base((PrionType.Enum))
        {
            Options = options;
            Value = value;
        }
        public override JsonNode ToJson()
        {
            return JsonNode.Parse($"\"{ToString()}\"");
        }
        public override string ToString()
        {
            var options = Options.ToList();
            options.Sort();
            string res = string.Join(", ", options);
            return "enum: " + res + ": " + Value;
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
            var options = new HashSet<string>([.. sections[1].Split(',').Select(s => s.Trim())]);
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
            if (!options.Contains(selected))
            {
                node = new PrionError($"enum options '{string.Join(", ", options)}' do not contain selected option '{selected}'.");
                return false;
            }
            node = new PrionEnum(options, selected);
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
        public string GetValue()
        {
            return Value;
        }
        public bool TrySetValue(string value)
        {
            if (Options.Contains(value))
            {
                Value = value;
                return true;
            }
            return false;
        }
    }
}
