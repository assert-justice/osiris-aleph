
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using Godot;

public static class TileGroupData
{
    public static Dictionary<Vector2I, ulong> DeserializeAll(JsonObject obj)
    {
        Dictionary<Vector2I, ulong> tileData = [];
        List<KeyValuePair<string, JsonNode>> temp = [.. obj];
        foreach (var (key, val) in temp)
        {
            var bf = ulong.Parse(key);
            var ls = val.AsArray().Select(RojaUtils.NodeToVec2I);
            foreach (var coord in ls)
            {
                if (tileData.ContainsKey(coord))
                {
                    GD.PrintErr("While parsing tile data duplicate tile coordinates detected!");
                }
                else
                {
                    tileData[coord] = bf;
                }
            }
        }
        return tileData;
    }
    public static JsonObject SerializeAll(Dictionary<Vector2I, ulong> tileData)
    {
        Dictionary<ulong, List<Vector2I>> lookup = [];
        foreach (var (vec, bf) in tileData)
        {
            if(lookup.TryGetValue(bf, out List<Vector2I> ls))
            {
                ls.Add(vec);
            }
            else
            {
                lookup.Add(bf, [vec]);
            }
        }
        JsonObject obj = [];
        foreach (var (bf, ls) in lookup)
        {
            obj[bf.ToString()] = new JsonArray([.. ls.Select(v => v.ToString())]);
        }
        return obj;
    }
}