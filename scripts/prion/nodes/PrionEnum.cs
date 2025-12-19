using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;

namespace Prion.Node;

public class PrionEnum(HashSet<string> options, string value) : PrionNode
{
    public readonly HashSet<string> Options = options;
    string Value = value;

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
    public static bool TryFromString(string value, out PrionEnum node, out string error)
    {
        node = default;
        error = default;
        value = value.Trim();
        var sections = value.Split(':').Select(s => s.Trim()).ToArray();
        if(sections.Length != 3)
        {
            error = $"Could not parse enum. Expected 3 sections, found {sections.Length}";
            return false;
        }
        if (sections[0] != "enum")
        {
            error = $"Enum signature not present at start of string '{value}'.";
            return false;
        }
        var optionStrings = sections[1].Split(',').Select(s => s.Trim()).ToArray();
        HashSet<string> options = [.. optionStrings];
        if(optionStrings.Length != options.Count)
        {
            error = $"Enum options contain duplicates.";
            return false;
        }
        foreach (var option in options)
        {
            foreach (char c in option)
            {
                if(!PrionParseUtils.IsAlphanumericOrUnderscore(c))
                {
                    error = $"Unexpected character '{c}' in enum option '{option}'. only alphanumeric characters or underscores are allowed.";
                    return false;
                }
            }
        }
        string selected = sections[2];
        if (!options.Contains(selected))
        {
            error = $"Enum options '{string.Join(", ", options)}' do not contain selected option '{selected}'.";
            return false;
        }
        node = new PrionEnum(options, selected);
        return true;
    }
    public static bool TryFromJson(JsonNode jsonNode, out PrionEnum node, out string error)
    {
        node = default;
        error = default;
        if(jsonNode is null)
        {
            error = "Invalid json kind. Value cannot be null.";
            return false;
        }
        var kind = jsonNode.GetValueKind();
        if(kind == System.Text.Json.JsonValueKind.String)
        {
            if(jsonNode.AsValue().TryGetValue(out string sValue))
            {
                return TryFromString(sValue, out node, out error);
            }
            error = "Should be unreachable";
            return false;
        }
        else
        {
            error = "Invalid json kind";
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
