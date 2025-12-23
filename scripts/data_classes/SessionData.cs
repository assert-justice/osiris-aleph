using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Prion.Node;

namespace Osiris.DataClass;

public class SessionData : IDataClass<SessionData>
{
    public AssetLogData AssetLog = new();
    public Dictionary<Guid, UserData> Users = [];
    public Dictionary<Guid, ActorData> Actors = [];
    public Dictionary<Guid, HandoutData> Handouts = [];
    public Dictionary<Guid, MapData> Maps = [];
    public List<Event> Events = [];
    public Dictionary<Guid, LayerData> LayerIndex = [];
    public Dictionary<Guid, StampData> StampIndex = [];
    public Dictionary<Guid, StampDataToken> TokenIndex = [];
    public Dictionary<Guid, Event> EventIndex = [];
    public static bool TryFromNode(PrionNode node, out SessionData data)
    {
        data = new();
        if(!node.TryAs(out PrionDict dict)) return false;
        if(!dict.TryGet("asset_log", out PrionNode prionNode)) return false;
        if(!AssetLogData.TryFromNode(prionNode, out data.AssetLog)) return false;
        if(!dict.TryGet("users", out PrionArray prionArray)) return false;
        foreach (var item in prionArray.Value)
        {
            if(!UserData.TryFromNode(item, out UserData entry)) return false;
            data.Users.Add(entry.Id, entry);
        }
        if(!dict.TryGet("actors", out prionArray)) return false;
        foreach (var item in prionArray.Value)
        {
            if(!ActorData.TryFromNode(item, out ActorData entry)) return false;
            data.Actors.Add(entry.Id, entry);
        }
        if(!dict.TryGet("handouts", out prionArray)) return false;
        foreach (var item in prionArray.Value)
        {
            if(!HandoutData.TryFromNode(item, out HandoutData entry)) return false;
            data.Handouts.Add(entry.Id, entry);
        }
        if(!dict.TryGet("maps", out prionArray)) return false;
        foreach (var item in prionArray.Value)
        {
            if(!MapData.TryFromNode(item, out MapData entry)) return false;
            data.Maps.Add(entry.Id, entry);
        }
        if(!dict.TryGet("events", out prionArray)) return false;
        foreach (var item in prionArray.Value)
        {
            if(!Event.TryFromNode(item, out Event eventObj)) return false;
            data.Events.Add(eventObj);
        }
        data.GenerateIndex();
        return true;
    }

    public PrionNode ToNode()
    {
        PrionDict dict = new();
        dict.Set("asset_log", AssetLog.ToNode());
        dict.Set("users", new PrionArray([..Users.Values.Select(u => u.ToNode())]));
        dict.Set("actors", new PrionArray([..Actors.Values.Select(u => u.ToNode())]));
        dict.Set("handouts", new PrionArray([..Handouts.Values.Select(u => u.ToNode())]));
        dict.Set("maps", new PrionArray([..Maps.Values.Select(u => u.ToNode())]));
        dict.Set("events", new PrionArray([..Events.Select(e => e.ToNode())]));
        return dict;
    }
    public void GenerateIndex()
    {
        foreach (var map in Maps.Values)
        {
            foreach (var layer in map.Layers)
            {
                LayerIndex[layer.Id] = layer;
                foreach (var stamp in layer.Stamps)
                {
                    StampIndex[stamp.Id] = stamp;
                    if(stamp is StampDataToken token) TokenIndex[token.Id] = token;
                }
            }
        }
    }
    public string GetChecksum()
    {
        var str = ToNode().ToJson().ToJsonString();
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(str));
        return Encoding.UTF8.GetString(hashBytes);
    }
    public bool TryApplyEvent(Event eventObj)
    {
        if(EventIndex.ContainsKey(eventObj.Id)) return false;
        Events.Add(eventObj);
        EventIndex.Add(eventObj.Id, eventObj);
        return true;
    }
}
