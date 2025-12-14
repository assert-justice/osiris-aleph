using System.Collections.Generic;
using System.Text.Json.Nodes;
using Prion;

namespace Osiris
{
    public static class SchemaManager
    {
        static readonly Dictionary<string, PrionSchema> Schemas = [];
        public static bool TryGetSchema(string filename, out PrionSchema schema)
        {
            if(Schemas.TryGetValue(filename, out schema)) return true;
            else if (OsirisFileAccess.FileExists("scripts/schemas" + filename))
            {
                string str = OsirisFileAccess.ReadFile("scripts/schemas" + filename);
                var jsonNode = JsonNode.Parse(str);
                if(PrionNode.TryFromJson(jsonNode, out PrionNode prionNode))
                {
                    if(PrionSchema.TryFromNode(prionNode, out schema, out string _)) return true;
                }
            }
            return false;
        }
    }
}
