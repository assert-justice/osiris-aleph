using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionDict : PrionNode
    {
        public Dictionary<string, PrionNode> Dict = [];
        public PrionDict() : base(PrionType.Dict){}
        public PrionDict(Dictionary<string, PrionNode> dict) : base(PrionType.Dict)
        {
            Dict = dict;
        }
        public override JsonNode ToJson()
        {
            JsonObject obj = [];
            foreach (var (key, value) in Dict)
            {
                obj.Add(key, value.ToJson());
            }
            return obj;
        }
        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
        public static new bool TryFromString(string value, out PrionNode node)
        {
            node = new PrionError("dict string parsing not yet implemented");
            return false;
        }
        public static new bool TryFromJson(JsonNode jsonNode, out PrionNode node)
        {
            if(jsonNode is null)
            {
                node = new PrionError("Invalid json kind. Value cannot be null.");
                return false;
            }
            var kind = jsonNode.GetValueKind();
            if(kind != System.Text.Json.JsonValueKind.Object)
            {
                node = new PrionError("Invalid json kind");
                return false;
            }
            PrionDict prionDict = new();
            foreach (var (key, value) in jsonNode.AsObject())
            {
                if(!PrionNode.TryFromJson(value, out PrionNode pNode))
                {
                    node = pNode;
                    return false;
                }
                prionDict.Dict.Add(key, pNode);
            }
            node = prionDict;
            return true;
        }
    }
}
