
using System;
using System.Text.Json.Nodes;

public class StampDataToken: StampData
{
    public Guid ActorId;
    public bool IsUnique;
    public JsonObject Stats; // Todo: only use local stats when IsUnique is false. If IsUnique is true then queries should be redirected to the relevant actor object.

    public StampDataToken(JsonObject obj) : base(obj)
    {
        _Type = StampType.Token;
        ActorId = RojaUtils.ObjGetGuid(obj, "actor_id");
        IsUnique = RojaUtils.ObjGetBool(obj, "is_unique", false);
        Stats = RojaUtils.ObjGetObj(obj, "stats?");
    }
    public override JsonObject Serialize()
    {
        JsonObject obj = BaseSerializer();
        obj["actor_id"] = ActorId.ToString();
        obj["is_unique"] = IsUnique;
        if(!IsUnique) obj["stats"] = Stats;
        return obj;
    }
}