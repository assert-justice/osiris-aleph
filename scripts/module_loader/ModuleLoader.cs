using System.Text.Json.Nodes;
using Prion.Node;
using Prion.Schema;
using Osiris.Scripting;
using Jint;
using Osiris.DataClass;
using System;
using Osiris.DataClass.Tests;

namespace Osiris.ModuleLoader;

public static class ModuleLoader
{
    public static void Example()
    {
        // System loosely based on Pathfinder 2E
        string examplePath = "res://example_module";
        foreach (var filename in OsirisSystem.DirListFiles(examplePath + "/schemas"))
        {
            if(!filename.EndsWith(".json")) continue;
            string schemaSrc = OsirisSystem.ReadFile(examplePath + "/schemas/" + filename);
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
            PrionSchemaManager.RegisterSchema(prionSchema);
            // OsirisSystem.Log($"Loaded and registered schema for '{prionSchema.Name}'");
        }
        var actor = MockClass.MockActor();
        // OsirisSystem.Log(actor.Id);
        OsirisSystem.Session.Actors.Add(actor.Id, actor);
        bool foundMain = false;
        foreach (var filename in OsirisSystem.DirListFiles(examplePath + "/scripts"))
        {
            if(filename == "main.js") foundMain = true;
            if(!filename.EndsWith(".js")) continue;
            string jsSrc = OsirisSystem.ReadFile(examplePath + "/scripts/" + filename);
            string name = filename[..^3];
            if(!OsirisSystem.Vm.TryAddModule(name, jsSrc)) continue;
            // OsirisSystem.Log($"Loaded script '{filename}'");
        }
        if(!foundMain)
        {
            OsirisSystem.ReportError("Could not find a 'main.js' file at supplied path.");
            return;
        }
        if(!OsirisSystem.Vm.TryImportModule("main", out VmModule mainModule))return;
        mainModule.TryCall("init");
    }
}
