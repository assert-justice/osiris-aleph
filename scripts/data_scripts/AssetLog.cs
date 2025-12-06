
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;

public class AssetLog
{
    public Dictionary<string, HashSet<Guid>> Logs = [];
    public AssetLog(JsonObject obj)
    {
        List<KeyValuePair<string, JsonNode>> temp = [.. obj];
        foreach (var (filename, ls) in temp)
        {
            HashSet<Guid> ids = [.. ls.AsArray().Select(v => JsonUtils.NodeToGuid(v))];
            Logs.Add(filename, ids);
        }
    }
    public JsonObject Serialize()
    {
        JsonObject obj = [];
        foreach (var (filename, ids) in Logs)
        {
            obj[filename] = new JsonArray([.. ids.Select(id => id.ToString())]);
        }
        return obj;
    }
    public void Add(string filename, Guid id)
    {
        if(Logs.TryGetValue(filename, out HashSet<Guid> ids))
        {
            ids.Add(id);
        }
        else
        {
            Logs.Add(filename, [id]);
        }
    }
    public string[] GetVisibleFilenames(Guid id)
    {
        return [.. Logs.Where(p => p.Value.Contains(id)).Select(p => p.Key)];
    }
}