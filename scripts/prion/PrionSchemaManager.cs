using System;
using System.Collections.Generic;

namespace Prion;

public class PrionSchemaManager
{
    Dictionary<Type, PrionSchema> Schemas = [];
    public void RegisterSchema(Type type, PrionSchema schema)
    {
        Schemas[type] = schema;
    }
    public bool Validate<T>(PrionNode node, out string error)
    {
        if(!Schemas.TryGetValue(typeof(T), out PrionSchema schema))
        {
            error = $"Could not find schema for {typeof(T)}.";
            return false;
        }
        return schema.TryValidate(node, out error);
    }
}
