using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Prion.Node;

namespace Osiris.DataClass;

public class MapData(Guid id) : IDataClass<MapData>
{
    public readonly Guid Id = id;
    public string DisplayName = "[A Mysterious Location]";
    public Vector2I Size = new(12, 18);
    public float CellWidth = 64;
    public HashSet<Guid> UsersPresent = [];
    public PrionDict State = new();
    public bool LightingEnabled = false;
    public Color BackgroundColor = Colors.Black;
    public Color BorderColor = Colors.Black;
    public bool GridVisible = true;
    public Color GridColor = Colors.White;
    public float GridLineWidth = 2;
    public List<BlockerData> Blockers = [];
    public List<TileGroupData> TileGroups = [];
    public List<LayerData> Layers = [];
    public static bool TryFromNode(PrionNode node, out MapData data)
    {
        data = default;
        if(!node.TryAs(out PrionDict dict)) return false;
        if(!dict.TryGet("map_id", out Guid id)) return false;
        data = new(id);
        if(!dict.TryGet("display_name", out data.DisplayName)) return false;
        if(!dict.TryGet("size", out PrionVector2I v)) return false;
        data.Size = ConversionUtils.FromPrionVector2I(v);
        if(dict.TryGet("cell_width?", out float cellWidth)) data.CellWidth = cellWidth;
        if(!dict.TryGet("users_present", out data.UsersPresent)) return false;
        if(!dict.TryGet("state", out data.State)) return false;
        if(dict.TryGet("lighting_enabled?", out bool lightingEnabled)) data.LightingEnabled = lightingEnabled;
        if(dict.TryGet("background_color?", out PrionColor prionColor)) data.BackgroundColor = ConversionUtils.FromPrionColor(prionColor);
        if(dict.TryGet("border_color?", out prionColor)) data.BorderColor = ConversionUtils.FromPrionColor(prionColor);
        if(dict.TryGet("grid_visible?", out bool gridVisible)) data.GridVisible = gridVisible;
        if(dict.TryGet("grid_color?", out prionColor)) data.GridColor = ConversionUtils.FromPrionColor(prionColor);
        if(dict.TryGet("grid_line_width?", out float gridLineWidth)) data.GridLineWidth = gridLineWidth;
        if(!dict.TryGet("blockers", out PrionArray prionArray)) return false;
        foreach (var blockerNode in prionArray.Value)
        {
            if(!BlockerData.TryFromNode(blockerNode, out BlockerData blocker)) return false;
            data.Blockers.Add(blocker);
        }
        if(!dict.TryGet("tile_groups", out prionArray)) return false;
        foreach (var tileGroupNode in prionArray.Value)
        {
            if(!TileGroupData.TryFromNode(tileGroupNode, out TileGroupData tileGroup)) return false;
            data.TileGroups.Add(tileGroup);
        }
        if(!dict.TryGet("layers", out prionArray)) return false;
        foreach (var layerNode in prionArray.Value)
        {
            if(!LayerData.TryFromNode(layerNode, out LayerData layer)) return false;
            data.Layers.Add(layer);
        }
        return true;
    }

    public PrionNode ToNode()
    {
        PrionDict dict = new();
        dict.Set("map_id", Id);
        dict.Set("display_name", DisplayName);
        dict.Set("size", ConversionUtils.ToPrionVector2I(Size));
        dict.Set("cell_width?", CellWidth);
        dict.Set("users_present", UsersPresent);
        dict.Set("state", State);
        dict.Set("lighting_enabled?", LightingEnabled);
        dict.Set("background_color?", ConversionUtils.ToPrionColor(BackgroundColor));
        dict.Set("border_color?", ConversionUtils.ToPrionColor(BorderColor));
        dict.Set("grid_visible?", GridVisible);
        dict.Set("grid_color?", ConversionUtils.ToPrionColor(GridColor));
        dict.Set("grid_line_width?", GridLineWidth);
        dict.Set("blockers", new PrionArray([..Blockers.Select(b => b.ToNode())]));
        dict.Set("tile_groups", new PrionArray([..TileGroups.Select(b => b.ToNode())]));
        dict.Set("layers", new PrionArray([..Layers.Select(b => b.ToNode())]));
        return dict;
    }
}
