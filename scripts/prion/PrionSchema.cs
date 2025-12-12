using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionSchema : PrionNode
    {
        public readonly string Name;
        public readonly bool IsNullable;
        public readonly string KeyName;
        readonly PrionNode SchemaNode;

        PrionSchema(string name, PrionNode schema, bool isNullable = false, string keyName = "[No Key Name]") : base(PrionType.Schema)
        {
            Name = name;
            IsNullable = isNullable;
            KeyName = keyName;
            SchemaNode = schema;
        }
        public override JsonNode ToJson()
        {
            throw new System.NotImplementedException();
        }
        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
        public static bool TryInit(string name, PrionNode schema, out PrionNode result)
        {
            // Check that schema is a dict
            if(schema.Type != PrionType.Dict)
            {
                result = new PrionError("Top level node of a schema must be a dict");
                return false;
            }
            var dict = schema as PrionDict;
            // Check for the presence of name and version
            if(!dict.Dict.ContainsKey("name"))
            {
                result = new PrionError("Expected field 'name' in schema.");
                return false;
            }
            if(!dict.Dict.ContainsKey("version"))
            {
                result = new PrionError("Expected field 'version' in schema.");
                return false;
            }
            // Check for the presence of data and validate it.
            if(!dict.Dict.TryGetValue("data", out PrionNode value))
            {
                result = new PrionError("Expected field 'data' in schema.");
                return false;
            }
            return TryGenerateSchemaNode(value, out result);
        }
        public static new bool TryFromString(string value, out PrionNode node)
        {
            node = new PrionError("schema parsing not yet implemented");
            return false;
        }
        public bool TryValidate(PrionNode node, out PrionError error)
        {
            error = new("");
            if(node.Type != SchemaNode.Type)
            {
                error = new PrionError($"Invalid node type '{node.Type}', expected {SchemaNode.Type}");
                return false;
            }
            switch (node.Type)
            {
                case PrionType.Bitfield:
                case PrionType.Boolean:
                case PrionType.Color:
                case PrionType.Dynamic:
                case PrionType.F32:
                case PrionType.Guid:
                case PrionType.I32:
                case PrionType.Rect2I:
                case PrionType.String:
                case PrionType.Vector2I:
                    // Basic types require no further validation.
                    return true;
                case PrionType.Array:
                    return TryValidateArray(node as PrionArray, out error);
                case PrionType.Dict:
                    return TryValidateDict(node as PrionDict, out error);
                case PrionType.Enum:
                    return TryValidateEnum(node as PrionEnum, out error);
                case PrionType.Error:
                    error = node as PrionError;
                    return false;
                case PrionType.Schema:
                    // TODO: determine if this should be allowed
                    return false;
                // case PrionType.Union:
                //     return TryValidateUnion(node as PrionUnion, out error);
                case PrionType.Null:
                    if(IsNullable) return true;
                    error = new("Value is not nullable.");
                    return false;
                default:
                    return false;
            }
        }
        bool TryValidateArray(PrionArray array, out PrionError error)
        {
            PrionSchema childSchema = (SchemaNode as PrionArray).Array[0] as PrionSchema;
            for (int idx = 0; idx < array.Array.Count; idx++)
            {
                PrionNode child = array.Array[idx];
                if(!childSchema.TryValidate(child, out error))
                {
                    error.Add($"Array entry at index {idx} does not match the schema.");
                    return false;
                }
            }
            error = new("");
            return true;
        }
        bool TryValidateDict(PrionDict dict, out PrionError error)
        {
            HashSet<string> allKeys = [.. dict.Dict.Keys];
            foreach (var (key, value) in (SchemaNode as PrionDict).Dict)
            {
                allKeys.Remove(key);
                if(key.StartsWith('#')) continue;
                var schema = value as PrionSchema;
                if(dict.Dict.TryGetValue(key, out PrionNode node))
                {
                    if(!schema.TryValidate(node, out error))
                    {
                        error.Add($"Could not validate value of field {key}");
                        return false;
                    }
                }
                else
                {
                    if (!IsNullable)
                    {
                        error = new($"Non nullable value of field {key} is missing");
                        return false;
                    }
                }
            }
            if(allKeys.Count > 0)
            {
                error = new($"Unexpected keys in dictionary {allKeys}");
                return false;
            }
            error = new("");
            return true;
        }
        bool TryValidateEnum(PrionEnum enumNode, out PrionError error)
        {
            var schemaOptions = (SchemaNode as PrionEnum).Options;
            var options = enumNode.Options;
            if(schemaOptions.Length != options.Length)
            {
                error = new("Enum has the wrong number of options.");
                return false;
            }
            for (int idx = 0; idx < schemaOptions.Length; idx++)
            {
                if(schemaOptions[idx] != options[idx])
                {
                    error = new("Enum options do not match schema.");
                    return false;
                }
            }
            error = new("");
            return true;
        }
        // bool TryValidateUnion(PrionUnion union, out PrionError error)
        // {
        //     error = new("");
        //     return true;
        // }
        static bool TryGenerateSchemaNode(PrionNode node, out PrionNode result)
        {
            switch (node.Type)
            {
                case PrionType.Array:
                    return TryGenerateSchemaArray(node as PrionArray, out result);
                case PrionType.Dict:
                    return TryGenerateSchemaDict(node as PrionDict, out result);
                case PrionType.String:
                    return TryGenerateSchemaString(node as PrionString, out result);
                default:
                    result = new PrionError($"Type {node.Type} is not a valid schema type");
                    return false;
            }
        }
        static bool TryGenerateSchemaArray(PrionArray array, out PrionNode result)
        {
            if(array.Array.Count != 1)
            {
                result = new PrionError("Arrays in schemas can only have one element, the schema of all entries");
                return false;
            }
            if(TryGenerateSchemaNode(array.Array[0], out result)) return true;
            else return false;
        }
        static bool TryGenerateSchemaDict(PrionDict dict, out PrionNode result)
        {
            result = dict;
            foreach (var (key, node) in dict.Dict)
            {
                if(!TryGenerateSchemaNode(node, out result))
                {
                    return false;
                }
            }
            return false;
        }
        static bool TryGenerateSchemaString(PrionString str, out PrionNode result)
        {
            result = str;
            switch (str.Text)
            {
                case "bitfield":
                case "boolean":
                case "color":
                case "dynamic":
                case "f32":
                case "guid":
                case "i32":
                case "rect2i":
                case "string":
                case "vector2i":
                    return true;
                default:
                    if(str.Text.StartsWith("enum:")) return TryGenerateSchemaEnum(str.Text, out result);
                    result = new PrionError($"Invalid string in place of schema type, found {str.Text}");
                    return false;
            }
        }
        static bool TryGenerateSchemaEnum(string str, out PrionNode result)
        {
            // slice off "enum:"
            str = str[5..];
            string[] options = [.. str.Split(',').Select(s => s.Trim())];
            result = new PrionEnum(options);
            return true;
        }
    }
}
