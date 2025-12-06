
using System;
using System.Text.Json.Nodes;

public class StampDataToken(JsonObject obj, StampType stampType) : StampData(obj, stampType)
{
    public Guid ActorId = JsonUtils.ObjGetGuid(obj, "actor_id");
    public bool IsUnique = JsonUtils.ObjGetBool(obj, "is_unique", false);
    public JsonObject Stats = JsonUtils.ObjGetObj(obj, "stats?"); // Todo: only use local stats when IsUnique is false. If IsUnique is true then queries should be redirected to the relevant actor object.

    public override JsonObject Serialize()
    {
        JsonObject obj = BaseSerializer();
        obj["actor_id"] = ActorId.ToString();
        obj["is_unique"] = IsUnique;
        if(!IsUnique) obj["stats"] = Stats;
        return obj;
    }
}