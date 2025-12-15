using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionArray : PrionNode
    {
        public List<PrionNode> Array = [];
        public PrionArray() : base(PrionType.Array){}
        public PrionArray(List<PrionNode> array) : base(PrionType.Array)
        {
            Array = array;
        }

        public override JsonNode ToJson()
        {
            JsonArray array = [];
            foreach (var item in Array)
            {
                array.Add(item.ToJson());
            }
            return array;
        }
        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
        public static new bool TryFromString(string value, out PrionNode node)
        {
            node = new PrionError("array string parsing not yet implemented");
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
            PrionArray prionArray = new();
            if(kind == System.Text.Json.JsonValueKind.Array)
            {
                var array = jsonNode.AsArray();
                foreach (var item in array)
                {
                    if(!PrionNode.TryFromJson(item, out PrionNode pNode))
                    {
                        node = pNode;
                        return false;
                    }
                    prionArray.Array.Add(pNode);
                }
            }
            else
            {
                node = new PrionError("Invalid json kind");
                return false;
            }
            node = prionArray;
            return true;
        }
        public bool TryAs<T>(out T[] res) where T : PrionNode
        {
            res = default;
            List<T> values = [];
            foreach (var node in Array)
            {
                if(!node.TryAs(out T val)) return false;
                values.Add(val);
            }
            res = [.. values];
            return true;
        }
    }
}
