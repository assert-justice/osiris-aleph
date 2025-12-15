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
            string filepath = "res://scripts/schemas/" + filename;
            if(Schemas.TryGetValue(filename, out schema)) return true;
            else if (OsirisSystem.FileExists(filepath))
            {
                string str = OsirisSystem.ReadFile(filepath);
                var jsonNode = JsonNode.Parse(str);
                if(!PrionNode.TryFromJson(jsonNode, out PrionNode prionNode)) return false;
                if(!PrionSchema.TryFromNode(prionNode, out schema, out string error))
                {
                    OsirisSystem.ReportError(error);
                    return false;
                }
                Schemas.Add(filename, schema);
                return true;
            }
            return false;
        }
    }
}
