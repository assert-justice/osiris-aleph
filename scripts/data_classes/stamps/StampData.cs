using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Prion.Node;

namespace Osiris.DataClass;

public class StampData(Guid id) : IDataClass<StampData>
{
    public readonly Guid Id = id;
    public string DisplayName = "";
    public HashSet<Guid> ControlledBy = [];
    public Rect2I Rect = new();
    public float Angle = 0;
    public float VisionRadius = 0;
    public bool HasVision = false;
    public List<LightData> Lights = [];
    public StampDataImage ImageData;
    public StampDataText TextData;
    public StampDataToken TokenData;

    public static bool TryFromNode(PrionNode node, out StampData data)
    {
        data = default;
        if(!node.TryAs(out PrionDict dict)) return false;
        if(!dict.TryGet("stamp_id", out Guid id)) return false;
        data = new(id);
        if(!dict.TryGet("controlled_by", out HashSet<Guid> guids)) return false;
        foreach (var item in guids)
        {
            data.ControlledBy.Add(item);
        }
        if(!dict.TryGet("rect", out PrionRect2I rect)) return false;
        data.Rect = ConversionUtils.FromPrionRect2I(rect);
        if(!dict.TryGet("angle", out data.Angle)) return false;
        if(dict.TryGet("vision_radius?", out data.VisionRadius)) data.HasVision = true;
        if(dict.TryGet("lights?", out PrionArray prionArray))
        {
            foreach (var item in prionArray.Value)
            {
                if(!LightData.TryFromNode(item, out LightData light)) return false;
                data.Lights.Add(light);
            }
        }
        if(dict.TryGet("image_data?", out PrionDict prionDict))
        {
            if(!StampDataImage.TryFromNode(prionDict, out data.ImageData)) return false;
        }
        if(dict.TryGet("text_data?", out prionDict))
        {
            if(!StampDataText.TryFromNode(prionDict, out data.TextData)) return false;
        }
        if(dict.TryGet("token_data?", out prionDict))
        {
            if(!StampDataToken.TryFromNode(prionDict, out data.TokenData)) return false;
        }
        return true;
    }
    public PrionNode ToNode()
    {
        PrionDict dict = new();
        dict.Set("stamp_id", Id);
        dict.Set("controlled_by", ControlledBy);
        dict.Set("rect", ConversionUtils.ToPrionRect2I(Rect));
        dict.Set("angle", Angle);
        dict.Set("lights?", new PrionArray([.. Lights.Select(l => l.ToNode())]));
        if(HasVision) dict.Set("vision_radius?", VisionRadius);
        if(ImageData is not null) dict.Set("image_data?", ImageData.ToNode());
        if(TextData is not null) dict.Set("text_data?", TextData.ToNode());
        if(TokenData is not null) dict.Set("token_data?", TokenData.ToNode());
        return dict;
    }
}
