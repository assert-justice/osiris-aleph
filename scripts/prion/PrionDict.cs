using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool TryGet<T>(string key, out T value) where T : PrionNode
        {
            value = default;
            if(!Dict.TryGetValue(key, out PrionNode res)) return false;
            if(res is not T) return false;
            value = res as T;
            return true;
        }
        public bool TryGet(string key, out Guid value)
        {
            value = default;
            if(!TryGet(key, out PrionGuid res)) return false;
            value = res.Value;
            return true;
        }
        public T GetDefault<T>(string key, T defaultVal) where T : PrionNode
        {
            if(!TryGet(key, out T value)) return defaultVal;
            return value;
        }
        public string GetDefault(string key, string defaultVal)
        {
            if(!TryGet(key, out PrionString prionString)) return defaultVal;
            return prionString.Text;
        }
        public bool TryGetGuidHashSet(string key, out HashSet<Guid> guids)
        {
            guids = default;
            if(!TryGet(key, out PrionArray res)) return false;
            if(!res.TryAs(out PrionGuid[] prionGuids)) return false;
            guids = [.. prionGuids.Select(o => o.Value)];
            return true;
        }
        public void Set(string key, Guid guid)
        {
            Dict[key] = new PrionGuid(guid);
        }
        public void Set(string key, string str)
        {
            Dict[key] = new PrionString(str);
        }
    }
}
