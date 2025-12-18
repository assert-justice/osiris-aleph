using System;
using System.Collections.Generic;

namespace Prion.Schema;

public static class PrionSchemaManager
{
    static readonly Dictionary<Type, PrionSchema> SchemasByType = [];
    public static readonly Dictionary<string, PrionSchema> SchemasByName = [];
    public static void RegisterSchema(Type type, PrionSchema schema)
    {
        // PrionSchema.SetManager(this);
        SchemasByName[schema.Name] = schema;
        SchemasByType[type] = schema;
    }
    public static bool Validate(Type type, PrionNode node, out string error)
    {
        if(!SchemasByType.TryGetValue(type, out PrionSchema schema))
        {
            error = $"Could not find schema for {type}.";
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
