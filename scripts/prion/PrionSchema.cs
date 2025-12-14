using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionSchema
    {
        readonly PrionNode SchemaNode;
        PrionSchema(PrionNode schemaNode)
        {
            SchemaNode = schemaNode;
        }
        public static bool TryFromNode(PrionNode schemaNode, out string error)
        {
            error = "";
            // validate node
            if(schemaNode.Type != PrionType.Dict)
            {
                error = $"Top level of a schema must be a Dict. Found '{schemaNode.Type}'";
                return false;
            }
            var dict = schemaNode as PrionDict;
            if(!dict.Dict.TryGetValue("name", out PrionNode node))
            {
                error = "Top level of a schema contain a name field";
                return false;
            }
            if(node.Type != PrionType.String)
            {
                error = "Schema name field must be a string.";
                return false;
            }
            if(!dict.Dict.TryGetValue("version", out node))
            {
                error = "Top level of a schema contain a version field";
                return false;
            }
            if(node.Type != PrionType.String)
            {
                error = "Schema version field must be a string.";
                return false;
            }
            if(!dict.Dict.TryGetValue("data", out node))
            {
                error = "Top level of a schema contain a data field";
                return false;
            }
            if(node.Type != PrionType.Dict)
            {
                error = "Schema data field must be a dict.";
                return false;
            }
            return TryValidateSchemaDict(node as PrionDict, out error);
        }

        static bool TryValidateSchemaValue(PrionNode node, out string error)
        {
            switch (node.Type)
            {
                case PrionType.Boolean:
                case PrionType.Color:
                case PrionType.Enum:
                case PrionType.Error:
                case PrionType.F32:
                case PrionType.Guid:
                case PrionType.I32:
                case PrionType.Null:
                case PrionType.Rect2I:
                case PrionType.UBigInt:
                case PrionType.Vector2I:
                    error = $"Schema field cannot contain a node of type '{node.Type}'.";
                    return false;
                case PrionType.Array:
                    return TryValidateSchemaArray(node as PrionArray, out error);
                case PrionType.Dict:
                    return TryValidateSchemaDict(node as PrionDict, out error);
                case PrionType.String:
                    return TryValidateSchemaString((node as PrionString).ToString(), out error);
                default:
                    error = $"Unhandled type '{node.Type}'";
                    return false;
            }
        }
        static bool TryValidateSchemaDict(PrionDict dict, out string error)
        {
            foreach (var (key, value) in dict.Dict)
            {
                if(key.StartsWith('#')) continue;
                if(!TryValidateSchemaValue(value, out error)) return false;
            }
            error = "";
            return true;
        }
        static bool TryValidateSchemaArray(PrionArray array, out string error)
        {
            if(array.Array.Count != 1)
            {
                error = "Arrays in schemas can only have one element, the schema of all entries";
                return false;
            }
            return TryValidateSchemaValue(array.Array[0], out error);
        }
        static bool TryValidateSchemaString(string str, out string error)
        {
            error = "";
            switch (str)
            {
                case "boolean":
                case "color":
                case "dynamic":
                case "f32":
                case "guid":
                case "i32":
                case "rect2i":
                case "string":
                case "ubigint":
                case "vector2i":
                    return true;
                default:
                    error = $"Unhandled string '{str}'";
                    return false;
            }
        }
    }
}
