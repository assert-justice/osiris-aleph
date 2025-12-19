using System;
using Godot;
using Prion;
using Prion.Node;

namespace Osiris.DataClass;

public class BlockerData : IDataClass<BlockerData>
{
    public enum BlockerStatus
    {
        Wall,
        Open,
        Closed,
        Locked,
    }
    public Vector2I Start = Vector2I.Zero;
    public Vector2I End = Vector2I.Zero;
    public BlockerStatus Status = BlockerStatus.Wall;
    public bool Opaque = true;
    public bool BlocksProjectiles = true;
    static BlockerStatus StatusFromString(string status) => status switch
    {
        "wall" => BlockerStatus.Wall,
        "open" => BlockerStatus.Open,
        "closed" => BlockerStatus.Closed,
        "locked" => BlockerStatus.Locked,
        _ => throw new ArgumentOutOfRangeException(status, $"Not expected status value: {status}"),
    };
    public static bool TryFromNode(PrionNode node, out BlockerData data)
    {
        data = default;
        if(!node.TryAs(out PrionDict dict)) return false;
        data = new();
        if(!dict.TryGet("start", out PrionVector2I v2i)) return false;
        data.Start = new(v2i.X, v2i.Y);
        if(!dict.TryGet("end", out v2i)) return false;
        data.End = new(v2i.X, v2i.Y);
        if(!dict.TryGet("status", out PrionEnum prionEnum)) return false;
        //  status = prionEnum.GetValue();
        data.Status = StatusFromString(prionEnum.GetValue());
        if(!dict.TryGet("opaque?", out data.Opaque)) return false;
        if(!dict.TryGet("blocks_projectiles?", out data.BlocksProjectiles)) return false;
        return true;
    }
    public PrionNode ToNode()
    {
        PrionDict dict = new();
        dict.Set("start", new PrionVector2I(Start.X, Start.Y));
        dict.Set("end", new PrionVector2I(End.X, End.Y));
        dict.Set("status", new PrionEnum(["wall", "open", "closed", "locked"], Status.ToString().ToLower()));
        dict.Set("opaque?", Opaque);
        dict.Set("blocks_projectiles?", BlocksProjectiles);
        return dict;
    }

}
