using System.Text.Json.Nodes;

namespace Prion.Node;

public class PrionString : IPrionNode<PrionString>
{
    public string Value = "";
    public PrionString(){}
    public PrionString(string value){Value = value;}

    public static bool TryFromJson(JsonNode jsonNode, out PrionString node, out string error)
    {
        error = default;
        if(jsonNode.GetValueKind() == System.Text.Json.JsonValueKind.String)
        {
            if(jsonNode.AsValue().TryGetValue(out string value)){
                node = new(value);
                return true;
            }
        }
        node = new(jsonNode.ToJsonString());
        return true;
    }

    public static bool TryFromString(string str, out PrionString node, out string error)
    {
        throw new System.NotImplementedException();
    }

    public override string ToString()
    {
        return Value;
    }

    public JsonNode ToJson()
    {
        return JsonNode.Parse($"\"{this}\"");
    }
}