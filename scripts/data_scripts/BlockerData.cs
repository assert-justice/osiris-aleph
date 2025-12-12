
using System.Text.Json.Nodes;
using Godot;

public enum BlockerStatus
{
    Wall,
    Open,
    Closed,
    Locked,
}

public class BlockerData(JsonObject obj)
{
    static string[] blockerStatus = ["wall", "open", "closed", "locked"];
    public Vector2I Start = RojaUtils.ObjGetVec2I(obj, "start", Vector2I.Zero);
    public Vector2I End = RojaUtils.ObjGetVec2I(obj, "end", Vector2I.Zero);
    public BlockerStatus Status = (BlockerStatus)RojaUtils.ObjGetEnum(obj, "status", blockerStatus, 0);
    public bool Opaque = RojaUtils.ObjGetBool(obj, "opaque?", true);
    public bool BlocksProjectiles = RojaUtils.ObjGetBool(obj, "blocks_projectiles?", true);

    public JsonObject Serialize()
    {
        JsonObject obj = [];
        obj["start"] = Start.ToString();
        obj["end"] = End.ToString();
        obj["status"] = blockerStatus[(int)Status];
        obj["opaque?"] = Opaque;
        obj["blocks_projectiles?"] = BlocksProjectiles;
        return obj;
    }
}
