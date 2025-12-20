using System;
using Prion.Node;

namespace Osiris.DataClass;

public class StampDataToken(Guid id) : StampData(id)
{
    public Guid ActorId;
    public bool IsUnique;
    public PrionDict Stats;
    /*
    "actor_id": "guid",
    "is_unique": "boolean",
    "stats": "dynamic"
    */
    public override bool TryFinishFromNode(PrionDict prionDict)
    {
        if(!prionDict.TryGet("token_data?", out PrionDict data)) return false;
        if(!data.TryGet("actor_id", out ActorId)) return false;
        if(!data.TryGet("is_unique", out IsUnique)) return false;
        if(!data.TryGet("stats", out Stats)) return false;
        return true;
    }
    public override PrionNode ToNode()
    {
        var node = BaseToNode();
        PrionDict dict = new();
        dict.Set("actor_id", ActorId);
        dict.Set("is_unique", IsUnique);
        dict.Set("stats", Stats);
        node.Set("token_data?", dict);
        return node;
    }
}
