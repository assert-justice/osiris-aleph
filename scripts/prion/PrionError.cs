using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace Prion
{
    public class PrionError(string message) : PrionNode(PrionType.Error)
    {
        readonly List<string> Messages = [message];
        public override JsonNode ToJson()
        {
            throw new System.NotImplementedException();
        }
        public override string ToString()
        {
            throw new System.NotImplementedException();
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
