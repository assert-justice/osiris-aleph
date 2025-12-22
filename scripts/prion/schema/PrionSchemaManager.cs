using System;
using System.Collections.Generic;
using Prion.Node;

namespace Prion.Schema;

public static class PrionSchemaManager
{
    static readonly Dictionary<Type, PrionSchema> SchemasByType = [];
    public static readonly Dictionary<string, PrionSchema> SchemasByName = [];
    public static void RegisterSchema(PrionSchema schema, Type type)
    {
        SchemasByName[schema.Name] = schema;
        SchemasByType[type] = schema;
    }
    public static void RegisterSchema(PrionSchema schema)
    {
        SchemasByName[schema.Name] = schema;
    }
    public static bool Validate(Type type, PrionNode node, out string error)
    {
        if(!SchemasByType.TryGetValue(type, out PrionSchema schema))
        {
            error = $"Could not find schema for type '{type}'.";
            return false;
        }
        return schema.TryValidate(node, out error);
    }
    public static bool Validate(string name, PrionNode node, out string error)
    {
        if(!SchemasByName.TryGetValue(name, out PrionSchema schema))
        {
            error = $"Could not find schema for name '{name}'.";
            return false;
        }
        return schema.TryValidate(node, out error);
    }
    public static void Clear()
    {
        SchemasByType.Clear();
        SchemasByName.Clear();
    }
}
