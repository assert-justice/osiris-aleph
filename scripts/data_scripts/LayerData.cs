
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;

public class LayerData(JsonObject obj)
{
    public string DisplayName = RojaUtils.ObjGetString(obj, "display_name", "New Layer");
    public bool IsPlayerVisible = RojaUtils.ObjGetBool(obj, "is_player_visible", true);
    public List<StampData> Stamps = [.. RojaUtils.ObjGetArray(obj, "stamps").Select(s => StampDataParser.Parse(obj.AsObject()))];
    public JsonObject Serialize()
    {
        JsonObject obj = [];
        obj["display_name"] = DisplayName;
        obj["is_player_visible"] = IsPlayerVisible;
        obj["stamps"] = new JsonArray([.. Stamps.Select(s => s.Serialize())]);
        return obj;
    }
}
