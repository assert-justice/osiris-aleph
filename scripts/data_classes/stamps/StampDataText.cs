using System;
using Prion.Node;

namespace Osiris.DataClass;

public class StampDataText(Guid id) : StampData(id)
{
    public string Text = "";
    public string FontFilename = "";
    public string FontSize = "16 px";
    public string[] Margins = ["16 px", "16 px", "16 px", "16 px"];
    // int _ScaleMode = 0;
    // bool HasScaleMode = false;
    // public int ScaleMode
    // {
    //     get => _ScaleMode;
    //     set
    //     {
    //         _ScaleMode = value;
    //         HasScaleMode = true;
    //     }
    // }
    int _AutoWrapMode = 0;
    bool HasAutoWrapMode = false;
    public int AutoWrapMode
    {
        get => _AutoWrapMode;
        set
        {
            _AutoWrapMode = value;
            HasAutoWrapMode = true;
        }
    }
    public override bool TryFinishFromNode(PrionDict prionDict)
    {
        if(!prionDict.TryGet("text_data?", out PrionDict data)) return false;
        if(!data.TryGet("text", out Text)) return false;
        if(data.TryGet("font_filename?", out string fontFilename)) FontFilename = fontFilename;
        if(data.TryGet("font_size?", out string size)) FontSize = size; 
        if(data.TryGet("margin_left?", out string margin)) Margins[0] = margin; 
        if(data.TryGet("margin_right?", out margin)) Margins[1] = margin; 
        if(data.TryGet("margin_top?", out margin)) Margins[2] = margin; 
        if(data.TryGet("margin_bottom?", out margin)) Margins[3] = margin;
        // if(data.TryGet("scale_mode?", out PrionEnum prionEnum)) _ScaleMode = prionEnum.Index;
        if(data.TryGet("auto_wrap_mode?", out PrionEnum prionEnum)) _AutoWrapMode = prionEnum.Index;
        return true;
    }
    public override PrionNode ToNode()
    {
        var node = BaseToNode();
        PrionDict dict = new();
        dict.Set("text", Text);
        if(FontFilename.Length > 0) dict.Set("font_filename?", FontFilename);
        if(FontSize.Length > 0) dict.Set("font_size?", FontSize);
        if(Margins[0].Length > 0) dict.Set("margin_left?", Margins[0]);
        if(Margins[1].Length > 0) dict.Set("margin_right?", Margins[1]);
        if(Margins[2].Length > 0) dict.Set("margin_top?", Margins[2]);
        if(Margins[3].Length > 0) dict.Set("margin_bottom?", Margins[3]);
        if(HasAutoWrapMode || AutoWrapMode != 0)
        {
            PrionEnum.TryFromOptions("off, arbitrary, word, word_smart", AutoWrapMode, out PrionEnum prionEnum, out string _);
            dict.Set("auto_wrap_mode?", prionEnum);
        }
        node.Set("text_data?", dict);
        return node;
    }
}
