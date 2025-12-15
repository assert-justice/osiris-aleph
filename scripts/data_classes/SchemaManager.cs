using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using Prion;

namespace Osiris
{
    public static class SchemaManager
    {
        static readonly Dictionary<string, PrionSchema> Schemas = [];
        static readonly Dictionary<Type, PrionSchema> Bindings = [];
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
        public static void AddSchema<T>(string filename)
        {
            if(!TryGetSchema(filename, out PrionSchema schema))
            {
                OsirisSystem.ReportError($"Unable to load schema at '{filename}'");
                return;
            }
            Bindings.Add(typeof(T), schema);
        }
        public static bool TryFromNode<T>(PrionNode node, out T data) where T : class, IBaseData
        {
            data = default;
            if(!Bindings.TryGetValue(typeof(T), out PrionSchema schema))
            {
                OsirisSystem.ReportError($"Could not find corresponding schema for type '{typeof(T)}'.");
                return false;
            }
            if(!schema.TryValidate(node, out string error))
            {
                OsirisSystem.ReportError(error);
                return false;
            }
            if(!T.TryFromNode(node, out data))
            {
                OsirisSystem.ReportError("Node creation failed");
                return false;
            }
            return true;
        }
        public static bool TryToNode<T>(T data, out PrionNode node) where T : IBaseData
        {
            node = data.ToNode();
            if(!Bindings.TryGetValue(typeof(T), out PrionSchema schema))
            {
                OsirisSystem.ReportError($"Could not find corresponding schema for type {typeof(T)}.");
                return false;
            }
            if(!schema.TryValidate(node, out string error))
            {
                OsirisSystem.ReportError(error);
                return false;
            }
            return true;
        }
        public static void Clear()
        {
            Schemas.Clear();
            Bindings.Clear();
        }
    }
}
