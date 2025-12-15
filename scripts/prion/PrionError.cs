using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionError(string message) : PrionNode(PrionType.Error)
    {
        public readonly List<string> Messages = [message];
        public override JsonNode ToJson()
        {
            throw new System.NotImplementedException();
        }
        public override string ToString()
        {
            string res = "error: " + string.Join(',', Messages);
            return res;
        }
        public void Add(string message)
        {
            Messages.Add(message);
        }
        public string[] GetMessages()
        {
            return [.. Messages];
        }
    }
}
