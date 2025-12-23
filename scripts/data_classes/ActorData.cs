using System;
using System.Collections.Generic;
using System.Linq;
using Prion.Node;

namespace Osiris.DataClass;
public class ActorData : IDataClass<ActorData>
{
    public readonly Guid Id;
    public string DisplayName = "[Mysterious Figure]";
    public HashSet<Guid> ControlledBy = [];
    public string PortraitFilename = "";
    public string TokenFilename = "";
    public PrionDict Stats = new();
    public string Description = "They are very mysterious.";
    public Dictionary<string, string> State = [];

    public ActorData(Guid guid)
    {
        Id = guid;
        List<List<int>> GroupsOfBuddies = [];
        GroupsOfBuddies.Add([0]);
    }
    public static bool TryFromNode(PrionNode node, out ActorData data)
    {
        data = default;
        if(!node.TryAs(out PrionDict prionDict)) return false;
        if(!prionDict.TryGet("actor_id", out Guid guid)) return false;
        data = new(guid)
        {
            DisplayName = prionDict.GetDefault("display_name?", "[Mysterious Figure]"),
            PortraitFilename = prionDict.GetDefault("portrait_filename?", ""),
            TokenFilename = prionDict.GetDefault("token_filename?", ""),
            Description = prionDict.GetDefault("description?", "They are very mysterious."),
        };
        if(!prionDict.TryGet("controlled_by", out data.ControlledBy)) return false;
        if(!prionDict.TryGet("stats", out data.Stats)) return false;
        if(prionDict.TryGet("state?", out prionDict))
        {
            foreach (var (key, value) in prionDict.Value)
            {
                if(!value.TryAs(out PrionString prionString)) return false;
                data.State[key] = prionString.Value;
            }
        }
        return true;
    }

    public PrionNode ToNode()
    {
        PrionDict dict = new();
        dict.Set("actor_id", Id);
        dict.Set("display_name?", DisplayName);
        dict.Set("portrait_filename?", PortraitFilename);
        dict.Set("token_filename?", TokenFilename);
        dict.Set("description?", Description);
        dict.Value["controlled_by"] = new PrionArray([.. ControlledBy.Select(o => new PrionGuid(o))]);
        dict.Value["stats"] = Stats;
        PrionDict prionDict = new();
        foreach (var (key, value) in State)
        {
            prionDict.Set(key, new PrionString(value));
        }
        dict.Set("state?", prionDict);
        return dict;
    }
}
