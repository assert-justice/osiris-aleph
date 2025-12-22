using System.Text.Json.Nodes;
using Prion.Node;
using Prion.Schema;

namespace Osiris.ModuleLoader;

public static class ModuleLoader
{
    public static void Example()
    {
        // System loosely based on Pathfinder 2E
        string schemaDirPath = "res://example_module/schemas";
        foreach (var filename in OsirisSystem.DirListFiles(schemaDirPath))
        {
            string schemaSrc = OsirisSystem.ReadFile(schemaDirPath + "/" + filename);
            var schemaJson = JsonNode.Parse(schemaSrc);
            if(!PrionNode.TryFromJson(schemaJson, out PrionNode prionNode, out string error))
            {
                OsirisSystem.ReportError(error);
                continue;
            }
            if(!PrionSchema.TryFromPrionNode(prionNode, out PrionSchema prionSchema, out error))
            {
                OsirisSystem.ReportError(error);
                continue;
            }
            OsirisSystem.Log($"Loaded and registered schema for '{prionSchema.Name}'");
            PrionSchemaManager.RegisterSchema(prionSchema);
        }
    }
}
