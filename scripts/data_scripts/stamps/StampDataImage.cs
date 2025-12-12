
using System.Text.Json.Nodes;

public class StampDataImage : StampData
{
    public string TextureFilename = "";
    public int StretchMode = 0;
    static string[] StretchModes = ["scale", "tile", "keep", "keep_centered", "keep_aspect", "keep_aspect_centered", "keep_aspect_covered"];
    public StampDataImage(JsonObject obj) : base(obj)
    {
        _Type = StampType.Image;
        TextureFilename = RojaUtils.ObjGetString(obj, "texture_filename?", "");
        StretchMode = RojaUtils.ObjGetEnum(obj, "stretch_mode?", StretchModes, 0);
    }

    public override JsonObject Serialize()
    {
        JsonObject obj = BaseSerializer();
        obj["texture_filename?"] = TextureFilename;
        obj["stretch_mode?"] = StretchModes[StretchMode];
        return obj;
    }
}