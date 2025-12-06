using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using Godot;

public class MapData(JsonObject obj)
{
    public Guid MapId = JsonUtils.ObjGetGuid(obj, "map_id");
    public string DisplayName = JsonUtils.ObjGetString(obj, "display_name", "New Map");
    public Vector2I Size = JsonUtils.ObjGetVec2I(obj, "size", new(20, 10));
    public int CellWidth = JsonUtils.ObjGetInt(obj, "cell_width", 128);
    public List<Guid> UsersPresent = [.. JsonUtils.ObjGetArray(obj, "users_present").Select(n => JsonUtils.NodeToGuid(n))];
    public JsonObject State = JsonUtils.ObjGetObj(obj, "state?");
    public bool FogEnabled = JsonUtils.ObjGetBool(obj, "fog_enabled?", false);
    public Color BackgroundColor = JsonUtils.ObjGetColor(obj, "background_color?", Colors.Gray);
    public Color BorderColor = JsonUtils.ObjGetColor(obj, "border_color?", Colors.Gray);
    public bool ShowGrid = JsonUtils.ObjGetBool(obj, "show_grid?", true);
    public Color GridColor = JsonUtils.ObjGetColor(obj, "grid_color?", Colors.White);
    public float GridWidth = JsonUtils.ObjGetFloat(obj, "grid_width?", 2);
    public List<BlockerData> Blockers = [.. JsonUtils.ObjGetArray(obj, "blockers").Select(o => new BlockerData(o.AsObject()))];
    public Dictionary<Vector2I, ulong> TileData = TileGroupData.DeserializeAll(JsonUtils.ObjGetObj(obj, "tiles"));
    public List<LayerData> Layers = [.. JsonUtils.ObjGetArray(obj, "layers").Select(o => new LayerData(o.AsObject()))];
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
