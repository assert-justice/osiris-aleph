using System.Text.Json.Nodes;

namespace Prion.Node;

public interface IPrionNode<T> where T : class
{
    public static abstract bool TryFromJson(JsonNode jsonNode, out T node, out string error);
    public static abstract bool TryFromString(string str, out T node, out string error);
    public string ToString();
    public JsonNode ToJson();
}
