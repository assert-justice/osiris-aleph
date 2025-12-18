using System.Collections.Generic;
using System.Linq;

namespace Prion.Schema;

public class PrionSchemaEnum : PrionSchemaNode
{
    readonly string[] Options;
    PrionSchemaEnum(string[] options)
    {
        Options = options;
    }
    public static bool TryFromString(string str, out PrionSchemaEnum prionSchemaEnum, out string error)
    {
        prionSchemaEnum = default;
        error = default;
        str = str[12..]; // remove "schema_enum:"
        var optionsList = str.Split(',').Select(s => s.Trim()).ToList();
        if(new HashSet<string>([..optionsList]).Count != optionsList.Count)
        {
            error = "Schema enum contains duplicates";
            return false;
        }
        optionsList.Sort();
        prionSchemaEnum = new([..optionsList]);
        return true;
    }
    public override bool TryValidate(PrionNode prionNode, out string error)
    {
        error = default;
        if(!prionNode.TryAs(out PrionEnum prionEnum))
        {
            error = $"Expected an enum, found a '{prionNode.GetType()}'.";
            return false;
        }
        var options = prionEnum.Options.ToList();
        if(Options.Length != options.Count)
        {
            error = "Enum options do not match schema.";
            return false;
        }
        options.Sort();
        for (int idx = 0; idx < Options.Length; idx++)
        {
            if(Options[idx] != options[idx])
            {
                error = "Enum options do not match schema.";
                return false;
            }
        }
        return true;
    }
}