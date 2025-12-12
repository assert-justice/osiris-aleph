using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace Prion
{
    // public class PrionUnionValue(string name, PrionNode contents)
    // {
    //     public readonly string Name = name;
    //     public readonly PrionNode Contents = contents;
    // }
    // public class PrionUnion : PrionNode
    // {
    //     readonly Dictionary<string, PrionSchema> Schemas;
    //     PrionUnionValue Value;
    //     PrionUnion(Dictionary<string, PrionSchema> schemas, PrionUnionValue value) : base(PrionType.Union)
    //     {
    //         Schemas = schemas;
    //         Value = value;
    //     }
    //     public override JsonNode ToJson()
    //     {
    //         throw new System.NotImplementedException();
    //     }
    //     public override string ToString()
    //     {
    //         throw new System.NotImplementedException();
    //     }
    //     public static new bool TryFromString(string value, out PrionNode node)
    //     {
    //         node = new PrionError("union parsing not yet implemented");
    //         return false;
    //     }
    //     public static bool TryInit(PrionSchema[] schemas, PrionUnionValue value, out PrionNode node)
    //     {
    //         Dictionary<string, PrionSchema> schemaDict = [];
    //         for (int idx = 0; idx < schemas.Length; idx++)
    //         {
    //             schemaDict.Add(schemas[idx].Name, schemas[idx]);
    //         }
    //         // validate value actually satisfies the schema it's supposed to.
    //         if(schemaDict.TryGetValue(value.Name, out PrionSchema schema))
    //         {
    //             if(schema.TryValidate(value.Contents, out PrionError error))
    //             {
    //                 node = new PrionUnion(schemaDict, value);
    //                 return true;
    //             }
    //             else
    //             {
    //                 node = new PrionError($"Invalid contents of value, does not match associated schema.");
    //                 return false;
    //             }
    //         }
    //         else
    //         {
    //             node = new PrionError($"Name {value.Name} is not a valid union type");
    //             return false;
    //         }
    //     }
    //     public PrionUnionValue GetValue()
    //     {
    //         return Value;
    //     }
    //     public bool TrySetValue(PrionUnionValue value, out PrionNode node)
    //     {
    //         node = new PrionNull();
    //         if(Schemas.TryGetValue(value.Name, out PrionSchema schema))
    //         {
    //             if(schema.TryValidate(value.Contents, out PrionError error))
    //             {
    //                 Value = value;
    //                 return true;
    //             }
    //             else
    //             {
    //                 node = error;
    //             }
    //         }
    //         return false;
    //     }
    // }
}
