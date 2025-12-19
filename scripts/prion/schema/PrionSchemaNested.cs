using System.Linq;
using Prion.Node;

namespace Prion.Schema;

public class PrionSchemaNested : PrionSchemaNode
{
    public static bool TryFromString(string str, out PrionSchemaNested prionSchemaNested, out string error)
    {
        prionSchemaNested = default;
        error = default;
        str = str[7..]; // remove "schema:"
        var schemaNames = str.Split(',').Select(s => s.Trim());
        
        return true;
    }
    public override bool TryValidate(PrionNode prionNode, out string error)
    {
        throw new System.NotImplementedException();
    }
}
