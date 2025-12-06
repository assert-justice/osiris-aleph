
using System.Text.Json.Nodes;
using Godot;

public class BlockerData(JsonObject obj)
{
    public Vector2I Start = JsonUtils.ObjGetVec2I(obj, "start", Vector2I.Zero);
    public Vector2I Stop = JsonUtils.ObjGetVec2I(obj, "end", Vector2I.Zero);
    public string Status = JsonUtils.ObjGetString(obj, "status", "wall");
    public BlockerProperties Properties = new(JsonUtils.ObjGetObj(obj, "properties?"));

    public JsonObject Serialize()
    {
        JsonObject obj = [];
        obj["start"] = Start.ToString();
        obj["end"] = Start.ToString();
        obj["status"] = Status;
        obj["properties?"] = Properties.Serialize();
        return obj;
    }
}
