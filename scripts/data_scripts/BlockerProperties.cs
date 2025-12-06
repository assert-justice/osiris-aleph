
using System.Text.Json.Nodes;

public class BlockerProperties(JsonObject obj)
{
    public bool Opaque = JsonUtils.ObjGetBool(obj, "opaque?", true);
    public bool BlocksProjectiles = JsonUtils.ObjGetBool(obj, "blocks_projectiles?", true);
    public JsonObject Serialize()
    {
        JsonObject obj = [];
        obj["opaque?"] = Opaque;
        obj["blocks_projectiles?"] = BlocksProjectiles;
        return obj;
    }
}