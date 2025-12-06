
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using Godot;

public abstract class StampData(JsonObject obj, StampType stampType)
{
    public readonly Guid Id = JsonUtils.ObjGetGuid(obj, "stamp_id");
    // public StampType Type = (StampType)JsonUtils.ObjGetEnum(obj, "type", StampTypeLookup, 0);
    public readonly StampType Type = stampType;
    public Rect2I Rect = JsonUtils.ObjGetRect2I(obj, "rect", new(Vector2I.Zero, Vector2I.One));
    public float Angle = JsonUtils.ObjGetFloat(obj, "angle", 0);
    public List<AuraData> Auras = [.. JsonUtils.ObjGetArray(obj, "auras?").Select(o => new AuraData(o.AsObject()))];
    public float VisionRadius = JsonUtils.ObjGetFloat(obj, "vision_radius?", 0);
    public float LightRadius = JsonUtils.ObjGetFloat(obj, "light_radius?", 0);
    public Color LightColor = JsonUtils.ObjGetColor(obj, "light_color?", Colors.White);

    protected JsonObject BaseSerializer()
    {
        JsonObject obj = [];
        obj["stamp_id"] = Id.ToString();
        obj["stamp_type"] = Type.ToString().ToLower();
        obj["rect"] = Rect.ToString();
        obj["angle"] = Angle;
        obj["auras?"] = new JsonArray([.. Auras.Select(a => a.Serialize())]);
        obj["vision_radius?"] = VisionRadius;
        obj["light_radius?"] = LightRadius;
        obj["light_color?"] = LightColor.ToHtml();
        return obj;
    }
    public abstract JsonObject Serialize();
}
