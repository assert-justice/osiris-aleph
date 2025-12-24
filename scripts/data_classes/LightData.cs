using System;
using Godot;
using Prion.Node;

namespace Osiris.DataClass;

public class LightData(Guid id) : IDataClass<LightData>
{
    public readonly Guid Id = id;
    public float Radius = 0;
    public Color Color = Colors.White;
    public static bool TryFromNode(PrionNode node, out LightData data)
    {
        data = default;
        if(node.TryAs(out PrionDict dict)) return false;
        if(!dict.TryGet("light_id", out Guid id)) return false;
        data = new(id);
        if(!dict.TryGet("radius", out data.Radius)) return false;
        if(dict.TryGet("color", out PrionColor color)) data.Color = ConversionUtils.FromPrionColor(color);
        return true;
    }

    public PrionNode ToNode()
    {
        PrionDict dict = new();
        dict.Set("light_id", Id);
        dict.Set("radius", Radius);
        dict.Set("color?", ConversionUtils.ToPrionColor(Color));
        return dict;
    }
}