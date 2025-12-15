using System.Collections.Generic;
using System.Linq;

namespace Prion
{
    public class PrionSchema
    {
        public readonly string Name;
        public readonly string Version;
        readonly PrionNode SchemaNode;
        static readonly Dictionary<string, PrionType> StringLookup = [];
        PrionSchema(string name, string version, PrionNode schemaNode)
        {
            Name = name;
            Version = version;
            SchemaNode = schemaNode;
        }
        public bool TryValidate(PrionNode node, out string error)
        {
            return TryValidateNode(SchemaNode, node, out error);
        }
        public static bool TryFromNode(PrionNode schemaNode, out PrionSchema schema, out string error)
        {
            // validate node
            schema = null;
            string name;
            string version;
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
            name = node.ToString();
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
            version = node.ToString();
            if(!dict.Dict.TryGetValue("data", out node))
            {
                error = "Top level of a schema contain a data field";
                return false;
            }
            if(!TryValidateSchemaNode(node, out error))
            {
                return false;
            }
            schema = new PrionSchema(name, version, node);
            return true;
        }

        static bool TryValidateSchemaNode(PrionNode node, out string error)
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
                if(!TryValidateSchemaNode(value, out error)) return false;
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
            return TryValidateSchemaNode(array.Array[0], out error);
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
        static bool TryValidateNode(PrionNode schemaNode, PrionNode userNode, out string error)
        {
            if(schemaNode == null)
            {
                error = "null schema node";
                return false;
            }
            if(userNode == null)
            {
                error = "null user node";
                return false;
            }
            if(schemaNode.Type == PrionType.String)
            {
                string schema = schemaNode.ToString();
                if(schema.StartsWith("enum:")) return TryValidateEnum(schema, userNode as PrionEnum, out error);
                else return TryValidateString(schema, userNode, out error);
            }
            else if(schemaNode.Type != userNode.Type)
            {
                error = $"Mismatched types, expected '{schemaNode.Type}' but found '{userNode.Type}'";
                return false;
            }
            switch (schemaNode.Type)
            {
                case PrionType.Array:
                    return TryValidateArray((schemaNode as PrionArray).Array[0], userNode as PrionArray, out error);
                case PrionType.Dict:
                    return TryValidateDict(schemaNode as PrionDict, userNode as PrionDict, out error);
                default:
                    break;
            }
            error = "";
            return true;
        }
        static bool TryValidateArray(PrionNode schemaNode, PrionArray userArray, out string error)
        {
            error = "";
            foreach (var item in userArray.Array)
            {
                if(!TryValidateNode(schemaNode, item, out error)) return false;
            }
            return true;
        }
        static bool TryValidateDict(PrionDict schemaDict, PrionDict userDict, out string error)
        {
            error = "";
            HashSet<string> keys = [.. schemaDict.Dict.Keys];
            foreach (var (key, value) in schemaDict.Dict)
            {
                keys.Remove(key);
                bool nullable = key.EndsWith('?');
                if(!userDict.Dict.TryGetValue(key, out PrionNode node))
                {
                    if(nullable) continue;
                    error = $"Dict is missing key from schema {key}";
                    return false;
                }
                if(nullable && node.Type == PrionType.Null) continue;
                if(value is PrionString str && str.Text == "dynamic") continue;
                if(!TryValidateNode(value, node, out error)) return false;
            }
            return true;
        }
        static bool TryValidateEnum(string schema, PrionEnum userEnum, out string error)
        {
            error = "";
            schema = schema[5..]; // remove "enum:"
            var optionsArray = schema.Split(',').Select(s => s.Trim()).ToArray();
            HashSet<string> optionsSet = [.. optionsArray];
            if(optionsArray.Length != userEnum.Options.Count)
            {
                error = "Enum options do not match schema.";
                return false;
            }
            if(optionsArray.Length != optionsSet.Count)
            {
                error = "Enum options contain duplicates";
                return false;
            }
            if(optionsSet.Union(userEnum.Options).Count() != optionsArray.Length)
            {
                error = "Enum options do not match schema.";
                return false;
            }
            return true;
        }
        static bool TryValidateString(string schema, PrionNode userNode, out string error)
        {
            if(StringLookup.Count == 0)
            {
                StringLookup.Add("boolean", PrionType.Boolean);
                StringLookup.Add("color", PrionType.Color);
                StringLookup.Add("f32", PrionType.F32);
                StringLookup.Add("guid", PrionType.Guid);
                StringLookup.Add("i32", PrionType.I32);
                StringLookup.Add("rect2i", PrionType.Rect2I);
                StringLookup.Add("string", PrionType.String);
                StringLookup.Add("ubigint", PrionType.UBigInt);
                StringLookup.Add("vector2i", PrionType.Vector2I);
            }
            if(StringLookup[schema] != userNode.Type)
            {
                error = $"Mismatched types 2, expected a '{schema}', received a '{userNode.Type}'.";
                return false;
            }
            error = "";
            return true;
        }
    }
}
