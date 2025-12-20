using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Prion.Node;

namespace Osiris.DataClass;

public abstract class StampData : IDataClass<StampData>
{
    public readonly Guid Id;
    public readonly HashSet<Guid> ControlledBy = [];
    public Rect2I Rect = new();
    public float Angle = 0;
    public float VisionRadius = 0;
    public bool HasVision = false;
    public float LightRadius = 0;
    public bool HasLight = false;
    public Color LightColor = Colors.White;
    public PrionDict Effects = new();
    public StampData()
    {
        Id = Guid.NewGuid();
    }
    public StampData(Guid id)
    {
        Id = id;
    }
    public static bool TryFromNode(PrionNode node, out StampData data)
    {
        throw new NotImplementedException();
    }
    public static bool TryFromNode<T>(PrionNode node, out T data) where T : StampData
    {
        throw new System.NotImplementedException();
    }

    protected PrionDict BaseToNode()
    {
        PrionDict res = new();
        res.Set("stamp_id", Id);
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
    public abstract PrionNode ToNode();

}