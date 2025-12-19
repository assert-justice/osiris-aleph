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

    public ActorData()
    {
        Id = Guid.NewGuid();
    }
    public ActorData(Guid guid)
    {
        Id = guid;
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
        return true;
    }
    public PrionNode ToNode()
    {
        PrionDict prionDict = new();
        prionDict.Set("actor_id", Id);
        prionDict.Set("display_name?", DisplayName);
        prionDict.Set("portrait_filename?", PortraitFilename);
        prionDict.Set("token_filename?", TokenFilename);
        prionDict.Set("description?", Description);
        prionDict.Value["controlled_by"] = new PrionArray([.. ControlledBy.Select(o => new PrionGuid(o))]);
        prionDict.Value["stats"] = Stats;
        return prionDict;
    }
}
