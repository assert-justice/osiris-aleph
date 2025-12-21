using System;
using Prion.Node;

namespace Osiris.DataClass;

public class StampDataText(Guid id) : StampData(id)
{
    public enum TextWrapMode
    {
        Off,
        Arbitrary,
        Word,
        WordSmart,
    }
    public string Text = "";
    public string FontFilename = "";
    public string FontSize = "16 px";
    public string[] Margins = ["16 px", "16 px", "16 px", "16 px"];
    // TODO: figure out scale mode stuff
    public TextWrapMode WrapMode = TextWrapMode.Off;
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
        if(data.TryGet("wrap_mode?", out PrionEnum prionEnum)) WrapMode = (TextWrapMode)prionEnum.Index;
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
        PrionEnum.TryFromOptions("off, arbitrary, word, word_smart", (int)WrapMode, out PrionEnum prionEnum, out string _);
        dict.Set("wrap_mode?", prionEnum);
        node.Set("text_data?", dict);
        return node;
    }
}
