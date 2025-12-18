using System;
using Godot;
using Prion;

namespace Osiris.DataClass;

public class BlockerData : IDataClass
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
    static bool IDataClass.TryFromNodeInternal<T>(PrionNode node, out T data)
    {
        data = default;
        if(!node.TryAs(out PrionDict dict)) return false;
        BlockerData blocker = new();
        if(!dict.TryGet("start", out PrionVector2I v2i)) return false;
        blocker.Start = new(v2i.X, v2i.Y);
        if(!dict.TryGet("end", out v2i)) return false;
        blocker.End = new(v2i.X, v2i.Y);
        if(!dict.TryGet("status", out PrionEnum prionEnum)) return false;
        //  status = prionEnum.GetValue();
        blocker.Status = StatusFromString(prionEnum.GetValue());
        if(!dict.TryGet("opaque?", out blocker.Opaque)) return false;
        if(!dict.TryGet("blocks_projectiles?", out blocker.BlocksProjectiles)) return false;
        data = blocker as T;
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