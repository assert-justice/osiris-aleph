using System;
using Prion.Node;

namespace Osiris.DataClass;

public class StampDataImage(Guid id) : StampData(id)
{
    public string ImageFilename = "";
    int _StretchMode = 0; // godot enum: scale, tile, keep, keep_centered, keep_aspect, keep_aspect_centered, keep_aspect_covered
    public int StretchMode
    {
        get => _StretchMode;
        set {
            HasStretchMode = true;
            _StretchMode = value;
        }
    }
    bool HasStretchMode = false;
    public override bool TryFinishFromNode(PrionDict prionDict)
    {
        if(!prionDict.TryGet("image_data?", out PrionDict data)) return false;
        if(data.TryGet("image_filename?", out string imageFilename)) ImageFilename = imageFilename;
        if(data.TryGet("stretch_mode?", out PrionEnum prionEnum))
        {
            StretchMode = prionEnum.Index;
            HasStretchMode = true;
        }
        return true;
    }
    public override PrionNode ToNode()
    {
        var node = BaseToNode();
        PrionDict dict = new();
        if(ImageFilename.Length > 0) dict.Set("image_filename?", ImageFilename);
        if(StretchMode != 0 || HasStretchMode)
        {
            string options = "scale, tile, keep, keep_centered, keep_aspect, keep_aspect_centered, keep_aspect_covered";
            PrionEnum.TryFromOptions(options, StretchMode, out PrionEnum prionEnum, out string _);
            dict.Set("stretch_mode?", prionEnum);
        }
        node.Set("image_data?", dict);
        return node;
    }
}
