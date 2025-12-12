using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using Godot;

public class MapData(JsonObject obj)
{
    public Guid MapId = RojaUtils.ObjGetGuid(obj, "map_id");
    public string DisplayName = RojaUtils.ObjGetString(obj, "display_name", "New Map");
    public Vector2I Size = RojaUtils.ObjGetVec2I(obj, "size", new(20, 10));
    public int CellWidth = RojaUtils.ObjGetInt(obj, "cell_width", 128);
    public List<Guid> UsersPresent = [.. RojaUtils.ObjGetArray(obj, "users_present").Select(n => RojaUtils.NodeToGuid(n))];
    public JsonObject State = RojaUtils.ObjGetObj(obj, "state?");
    public bool FogEnabled = RojaUtils.ObjGetBool(obj, "fog_enabled?", false);
    public Color BackgroundColor = RojaUtils.ObjGetColor(obj, "background_color?", Colors.Gray);
    public Color BorderColor = RojaUtils.ObjGetColor(obj, "border_color?", Colors.Gray);
    public bool ShowGrid = RojaUtils.ObjGetBool(obj, "show_grid?", true);
    public Color GridColor = RojaUtils.ObjGetColor(obj, "grid_color?", Colors.White);
    public float GridWidth = RojaUtils.ObjGetFloat(obj, "grid_width?", 2);
    public List<BlockerData> Blockers = [.. RojaUtils.ObjGetArray(obj, "blockers").Select(o => new BlockerData(o.AsObject()))];
    public Dictionary<Vector2I, ulong> TileData = TileGroupData.DeserializeAll(RojaUtils.ObjGetObj(obj, "tiles"));
    public List<LayerData> Layers = [.. RojaUtils.ObjGetArray(obj, "layers").Select(o => new LayerData(o.AsObject()))];
    public JsonObject Serialize()
    {
        JsonObject obj = [];
        obj["map_id"] = MapId.ToString();
        obj["display_name"] = DisplayName;
        obj["size"] = Size.ToString();
        obj["cell_width"] = CellWidth;
        obj["users_present"] = new JsonArray([.. UsersPresent.Select(id => id.ToString())]);
        obj["state?"] = State;
        obj["fog_enabled?"] = FogEnabled;
        obj["background_color?"] = BackgroundColor.ToHtml();
        obj["border_color?"] = BorderColor.ToHtml();
        obj["show_grid?"] = ShowGrid;
        obj["grid_color?"] = GridColor.ToHtml();
        obj["grid_width?"] = GridWidth;
        obj["blockers"] = new JsonArray([.. Blockers.Select(b => b.Serialize())]);
        obj["tile_data"] = TileGroupData.SerializeAll(TileData);
        obj["layers"] = new JsonArray([.. Layers.Select(s => s.Serialize())]);
        return obj;
    }
}
