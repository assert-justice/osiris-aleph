using System;
using System.Collections.Generic;
using System.Linq;
using Prion;

namespace Osiris.DataClass;
public class AssetLogData : IDataClass
{
    readonly Dictionary<string, HashSet<Guid>> Data = [];
    public AssetLogData(){}
    public AssetLogData(Dictionary<string, HashSet<Guid>> data)
    {
        Data = data;
    }
    static bool IDataClass.TryFromNodeInternal<T>(PrionNode node, out T res)
    {
        res = default;
        // return true;
        if(node.Type != PrionType.Array) return false;
        var arr = node as PrionArray;
        Dictionary<string, HashSet<Guid>> data = [];
        foreach (var item in arr.Array)
        {
            if(item.Type != PrionType.Dict) return false;
            var dict = item as PrionDict;
            if(!dict.TryGet("filename", out PrionString filenameNode)) return false;
            string filename = filenameNode.Text;
            if(!dict.TryGet("owners", out PrionArray ownersNode)) return false;
            HashSet<Guid> owners = [];
            foreach (var ownerNode in ownersNode.Array)
            {
                if(ownerNode.Type != PrionType.Guid) return false;
                owners.Add((ownerNode as PrionGuid).Value);
            }
            data.Add(filename, owners);
        }
        // assetLog = new(data);
        res = new AssetLogData(data) as T;
        return true;
    }

    public PrionNode ToNode()
    {
        PrionArray array = new();
        foreach (var (filename, owners) in Data)
        {
            PrionDict dict = new();
            dict.Dict.Add("filename", PrionString.FromString(filename));
            PrionArray arr = new();
            foreach (var owner in owners)
            {
                arr.Array.Add(PrionGuid.FromGuid(owner));
            }
            dict.Dict.Add("owners", arr);
            array.Array.Add(dict);
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
