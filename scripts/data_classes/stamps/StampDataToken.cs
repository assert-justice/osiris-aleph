using System;
using Prion.Node;

namespace Osiris.DataClass;

public class StampDataToken : IDataClass<StampDataToken>
{
    public Guid ActorId;
    public PrionDict Stats;
    public static bool TryFromNode(PrionNode node, out StampDataToken data)
    {
        data = default;
        if(!node.TryAs(out PrionDict dict)) return false;
        if (!dict.TryGet("actor_id", out Guid actorId)) return false;
        data = new()
        {
            ActorId = actorId,
            Stats = dict.GetDefault<PrionDict>("stats?", null),
        };
        return true;
    }

    public PrionNode ToNode()
    {
        PrionDict dict = new();
        dict.Set("actor_id", ActorId);
        dict.Set("stats?", Stats);
        return dict;
    }
}
