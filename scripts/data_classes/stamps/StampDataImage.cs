using System;
using Prion.Node;

namespace Osiris.DataClass;

public class StampDataImage : IDataClass<StampDataImage>
{
    public enum ImageStretchMode
    {
        Scale,
        Tile,
        Keep,
        KeepAspect,
        KeepAspectCentered,
        KeepAspectCovered,
    }
    public string ImageFilename = "";
    // godot enum: scale, tile, keep, keep_centered, keep_aspect, keep_aspect_centered, keep_aspect_covered
    public ImageStretchMode StretchMode = ImageStretchMode.Scale;
    public static bool TryFromNode(PrionNode node, out StampDataImage data)
    {
        data = default;
        if(!node.TryAs(out PrionDict dict)) return false;
        data = new();
        if(dict.TryGet("image_filename?", out string imageFilename)) data.ImageFilename = imageFilename;
        if(dict.TryGet("stretch_mode?", out PrionEnum prionEnum))
        {
            data.StretchMode = (ImageStretchMode)prionEnum.Index;
        }
        return true;
    }
    public PrionNode ToNode()
    {
        PrionDict dict = new();
        dict.Set("image_filename?", ImageFilename);
        string options = "scale, tile, keep, keep_centered, keep_aspect, keep_aspect_centered, keep_aspect_covered";
        PrionEnum.TryFromOptions(options, (int)StretchMode, out PrionEnum prionEnum, out string _);
        dict.Set("stretch_mode?", prionEnum);
        return dict;
    }
}
