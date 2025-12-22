using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace Prion.Node;

public class PrionArray : PrionNode
{
    public readonly List<PrionNode> Value = [];
    public PrionArray(){}
    public PrionArray(List<PrionNode> array){Value = array;}
    public static bool TryFromJson(JsonNode jsonNode, out PrionArray prionArray, out string error)
    {
        error = default;
        prionArray = default;
        if(jsonNode is null)
        {
            error = "Invalid json kind. Value cannot be null.";
            return false;
        }
        var kind = jsonNode.GetValueKind();
        prionArray = new();
        if(kind != System.Text.Json.JsonValueKind.Array)
        {
            error = "Invalid json kind";
            return false;
        }
        var array = jsonNode.AsArray();
        foreach (var item in array)
        {
            if(!TryFromJson(item, out PrionNode prionNode, out error)) return false;
            prionArray.Value.Add(prionNode);
        }
        return true;
    }
    public override JsonNode ToJson()
    {
        JsonArray array = [];
        foreach (var item in Value)
        {
            array.Add(item.ToJson());
        }
        return array;
    }

    public override string ToString()
    {
        return "array";
    }
    public bool TryAs<T>(out List<T> res) where T : PrionNode
    {
        res = [];
        foreach (var node in Value)
        {
            if(!node.TryAs(out T val)) return false;
            res.Add(val);
        }
        return true;
    }
}