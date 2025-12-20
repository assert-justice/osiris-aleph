using System;
using System.Collections.Generic;
using System.Linq;
using Prion.Node;

namespace Osiris.DataClass;
public class AssetLogData : IDataClass<AssetLogData>
{
    readonly Dictionary<string, HashSet<Guid>> Data = [];
    public AssetLogData(){}
    public AssetLogData(Dictionary<string, HashSet<Guid>> data)
    {
        Data = data;
    }
    public static bool TryFromNode(PrionNode node, out AssetLogData data)
    {
        data = default;
        if(!node.TryAs(out PrionArray prionArray)) return false;
        Dictionary<string, HashSet<Guid>> logs = [];
        foreach (var item in prionArray.Value)
        {
            if(!item.TryAs(out PrionDict dict)) return false;
            if(!dict.TryGet("filename", out string filename)) return false;
            if(!dict.TryGet("owners", out HashSet<Guid> owners)) return false;
            logs.Add(filename, owners);
        }
        data = new AssetLogData(logs);
        return true;
    }
    public PrionNode ToNode()
    {
        PrionArray array = new();
        foreach (var (filename, owners) in Data)
        {
            PrionDict dict = new();
            dict.Value.Add("filename", PrionString.FromString(filename));
            PrionArray arr = new();
            foreach (var owner in owners)
            {
                arr.Value.Add(new PrionGuid(owner));
            }
            dict.Value.Add("owners", arr);
            array.Value.Add(dict);
        }
        return array;
    }
    public bool IsOwner(string filename, Guid userId)
    {
        if(Data.TryGetValue(filename, out HashSet<Guid> owners))
        {
            return owners.Contains(userId);
        }
        return false;
    }
    public void Add(string filename, Guid userId)
    {
        if(Data.TryGetValue(filename, out HashSet<Guid> owners))
        {
            owners.Add(userId);
        }
        else
        {
            Data.Add(filename, [userId]);
        }
    }
    public bool Remove(string filename, Guid userId)
    {
        if(Data.TryGetValue(filename, out HashSet<Guid> owners))
        {
            return owners.Remove(userId);
        }
        return false;
    }
    public string[] GetVisibleFiles(Guid userId)
    {
        return [.. Data.Where( kv =>
        {
            var (_,v) = kv;
            return v.Contains(userId);
        }).Select(kv =>
        {
            var (k,_) = kv;
            return k;
        })];
    }
}
