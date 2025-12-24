using System;
using Prion.Node;

namespace Osiris.DataClass;

public class StampDataText : IDataClass<StampDataText>
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
    public static bool TryFromNode(PrionNode node, out StampDataText data)
    {
        data = default;
        if(!node.TryAs(out PrionDict dict)) return false;
        data = new();
        if(dict.TryGet("font_filename?", out string fontFilename)) data.FontFilename = fontFilename;
        if(dict.TryGet("font_size?", out string size)) data.FontSize = size; 
        if(dict.TryGet("margin_left?", out string margin)) data.Margins[0] = margin; 
        if(dict.TryGet("margin_right?", out margin)) data.Margins[1] = margin; 
        if(dict.TryGet("margin_top?", out margin)) data.Margins[2] = margin; 
        if(dict.TryGet("margin_bottom?", out margin)) data.Margins[3] = margin;
        if(dict.TryGet("wrap_mode?", out PrionEnum prionEnum)) data.WrapMode = (TextWrapMode)prionEnum.Index;
        return true;
    }

    public PrionNode ToNode()
    {
        PrionDict dict = new();
        dict.Set("text", Text);
        dict.Set("font_filename?", FontFilename);
        dict.Set("font_size?", FontSize);
        dict.Set("margin_left?", Margins[0]);
        dict.Set("margin_right?", Margins[1]);
        dict.Set("margin_top?", Margins[2]);
        dict.Set("margin_bottom?", Margins[3]);
        PrionEnum.TryFromOptions("off, arbitrary, word, word_smart", (int)WrapMode, out PrionEnum prionEnum, out string _);
        dict.Set("wrap_mode?", prionEnum);
        return dict;
    }
}
