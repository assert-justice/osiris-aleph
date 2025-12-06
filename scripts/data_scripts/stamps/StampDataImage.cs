
using System.Text.Json.Nodes;

public class StampDataImage : StampData
{
    public string TextureFilename = "";
    public int StretchMode = 0;
    static string[] StretchModes = ["scale", "tile", "keep", "keep_centered", "keep_aspect", "keep_aspect_centered", "keep_aspect_covered"];
    public StampDataImage(JsonObject obj, StampType stampType) : base(obj, stampType)
    {
        TextureFilename = JsonUtils.ObjGetString(obj, "texture_filename?", "");
        StretchMode = JsonUtils.ObjGetEnum(obj, "stretch_mode?", StretchModes, 0);
    }

    public override JsonObject Serialize()
    {
        JsonObject obj = BaseSerializer();
        obj["texture_filename?"] = TextureFilename;
        obj["stretch_mode?"] = StretchModes[StretchMode];
        return obj;
    }
}