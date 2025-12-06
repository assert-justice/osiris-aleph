
using System.Text.Json.Nodes;
using Godot;

public class BlockerData(JsonObject obj)
{
    public Vector2I Start = JsonUtils.ObjGetVec2I(obj, "start", Vector2I.Zero);
    public Vector2I Stop = JsonUtils.ObjGetVec2I(obj, "end", Vector2I.Zero);
    public string Status = JsonUtils.ObjGetString(obj, "status", "wall");
    public bool Opaque = JsonUtils.ObjGetBool(obj, "opaque?", true);
    public bool BlocksProjectiles = JsonUtils.ObjGetBool(obj, "blocks_projectiles?", true);

    public JsonObject Serialize()
    {
        JsonObject obj = [];
        obj["start"] = Start.ToString();
        obj["end"] = Start.ToString();
        obj["status"] = Status;
        obj["opaque?"] = Opaque;
        obj["blocks_projectiles?"] = BlocksProjectiles;
        return obj;
    }
}
