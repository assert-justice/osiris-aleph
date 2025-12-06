
using System.Linq;
using System.Text.Json.Nodes;

public enum TextScaleMode
{
    None,
    OnOverflow,
    Always,
}

public class StampDataText: StampData
{
    static string[] ScaleModes = ["none", "on_overflow", "always"];
    static string[] WrapModes = ["off", "arbitrary", "word", "word_smart"];
    public string Text = "";
    public string FontFilename = "";
    public float FontSizePx = 0;
    public float FontSizeTiles = 0;
    public float[] Margins = [0.1f, 0.1f, 0.1f, 0.1f];
    public TextScaleMode ScaleMode = TextScaleMode.None;
    public int AutoWrapMode = 0;
    public StampDataText(JsonObject obj, StampType stampType) : base(obj, stampType)
    {
        Text = JsonUtils.ObjGetString(obj, "text", "");
        FontFilename = JsonUtils.ObjGetString(obj, "font_filename?", "");
        FontSizePx = JsonUtils.ObjGetFloat(obj, "font_size_px?", 0);
        FontSizeTiles = JsonUtils.ObjGetFloat(obj, "font_size_tiles?", 0);
        float[] margins = [.. JsonUtils.ObjGetArray(obj, "margins?").Select(o => JsonUtils.NodeToFloat(o, 0.1f))];
        if(margins.Length == 4) Margins = margins;
        ScaleMode = (TextScaleMode)JsonUtils.ObjGetEnum(obj, "scale_mode?", ScaleModes, 0);
        AutoWrapMode = JsonUtils.ObjGetEnum(obj, "auto_wrap_mode?", WrapModes, 0);
    }

    public override JsonObject Serialize()
    {
        JsonObject obj = BaseSerializer();
        obj["text"] = Text;
        obj["font_filename?"] = FontFilename;
        obj["font_size_px?"] = FontSizePx;
        obj["font_size_tiles?"] = FontSizeTiles;
        obj["margins?"] = new JsonArray([.. Margins]);
        obj["scale_mode?"] = ScaleModes[(int)ScaleMode];
        obj["auto_wrap_mode"] = WrapModes[AutoWrapMode];
        return obj;
    }
}
