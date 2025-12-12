
using System;
using System.Text.Json.Nodes;

public abstract class EffectData(JsonObject obj, EffectType type)
{
    public readonly Guid Id = RojaUtils.ObjGetGuid(obj, "effect_type");
    public readonly EffectType Type = type;
    public readonly Guid SourceId = RojaUtils.ObjGetGuid(obj, "source_id");
    public readonly Guid ScriptId = RojaUtils.ObjGetGuid(obj, "script_id");

    protected JsonObject BaseSerializer()
    {
        JsonObject obj = [];
        obj["effect_id"] = Id.ToString();
        obj["type"] = Type.ToString().ToLower();
        obj["source_id"] = SourceId.ToString();
        obj["script_id"] = ScriptId.ToString();
        return obj;
    }

    public abstract JsonObject Serialize();
}
