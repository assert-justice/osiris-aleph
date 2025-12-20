using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Prion.Node;

namespace Osiris.DataClass;

public abstract class StampData(Guid id) : IDataClass<StampData>
{
    public readonly Guid Id = id;
    public readonly HashSet<Guid> ControlledBy = [];
    public Rect2I Rect = new();
    public float Angle = 0;
    public float VisionRadius = 0;
    public bool HasVision = false;
    public float LightRadius = 0;
    public bool HasLight = false;
    public Color LightColor = Colors.White;
    public PrionDict Effects = new();

    public static bool TryFromNode(PrionNode node, out StampData data)
    {
        data = default;
        if(!node.TryAs(out PrionDict dict)) return false;
        if(!dict.TryGet("stamp_id", out Guid id)) return false;
        if(!dict.TryGet("stamp_type", out PrionEnum typeEnum)) return false;
        data = InitStamp(id, typeEnum.GetValue());
        if(!dict.TryGet("controlled_by", out HashSet<Guid> guids)) return false;
        foreach (var item in guids)
        {
            data.ControlledBy.Add(item);
        }
        if(!dict.TryGet("rect", out PrionRect2I rect)) return false;
        data.Rect = ConversionUtils.FromPrionRect2I(rect);
        if(!dict.TryGet("angle", out data.Angle)) return false;
        if(dict.TryGet("vision_radius?", out data.VisionRadius)) data.HasVision = true;
        if(dict.TryGet("light_radius?", out data.LightRadius)) data.HasLight = true;
        if(dict.TryGet("light_color?", out PrionColor prionColor)) data.LightColor = ConversionUtils.FromPrionColor(prionColor);
        if(dict.TryGet("effects?", out PrionDict effects)) data.Effects = effects;
        return data.TryFinishFromNode(dict);
    }
    public static bool TryFromNode<T>(PrionNode node, out T data) where T : StampData
    {
        data = default;
        if(!TryFromNode(node, out StampData stampData)) return false;
        if(stampData is T res)
        {
            data = res;
            return true;
        }
        return false;
    }

    protected PrionDict BaseToNode()
    {
        PrionDict res = new();
        res.Set("stamp_id", Id);
        string name = GetType().ToString().Split("StampData")[1].ToLower();
        PrionEnum.TryFromOptions("text, image, token", name, out PrionEnum prionEnum, out string _);
        res.Set("stamp_type", prionEnum);
        res.Set("controlled_by", ControlledBy);
        res.Set("rect", ConversionUtils.ToPrionRect2I(Rect));
        res.Set("angle", Angle);
        if(HasVision) res.Set("vision_radius?", VisionRadius);
        if (HasLight)
        {
            res.Set("light_radius?", LightRadius);
            res.Set("light_color?", ConversionUtils.ToPrionColor(LightColor));
        }
        if(Effects.Value.Count > 0) res.Set("effects?", Effects);
        return res;
    }
    static StampData InitStamp(Guid id, string type)
    {
        switch (type)
        {
            case "text":
                return new StampDataText(id);
            case "image":
                return new StampDataImage(id);
            case "token":
                return new StampDataToken(id);
            default:
                OsirisSystem.ReportError($"Unexpected type for stamp '{type}'");
                return default;
        }
    }
    public abstract PrionNode ToNode();
    public abstract bool TryFinishFromNode(PrionDict prionDict);
}