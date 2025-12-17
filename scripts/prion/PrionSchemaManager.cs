using System;
using System.Collections.Generic;

namespace Prion;

public class PrionSchemaManager
{
    readonly Dictionary<Type, PrionSchema> SchemasByType = [];
    public readonly Dictionary<string, PrionSchema> SchemasByName = [];
    public void RegisterSchema(Type type, PrionSchema schema)
    {
        PrionSchema.SetManager(this);
        SchemasByName[schema.Name] = schema;
        SchemasByType[type] = schema;
    }
    public bool Validate<T>(PrionNode node, out string error)
    {
        if(!SchemasByType.TryGetValue(typeof(T), out PrionSchema schema))
        {
            error = $"Could not find schema for {typeof(T)}.";
            return false;
        }
        return schema.TryValidate(node, out error);
    }
}
