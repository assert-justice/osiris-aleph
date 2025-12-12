
using System;
using System.Linq;
using System.Text.Json.Nodes;

public class HandoutData(JsonObject obj)
{
    public Guid Id = RojaUtils.ObjGetGuid(obj, "handout_id");
    public string DisplayName = RojaUtils.ObjGetString(obj, "display_name?", "Mysterious Note");
    public string TextureFilename = RojaUtils.ObjGetString(obj, "texture_filename?", "");
    public string Text = RojaUtils.ObjGetString(obj, "text?", "");
    public Guid[] VisibleTo = [.. RojaUtils.ObjGetArray(obj, "visible_to").Select(o => RojaUtils.NodeToGuid(o))];
    public string GmText = RojaUtils.ObjGetString(obj, "gm_text?", "");

    public JsonObject Serialize()
    {
        JsonObject obj = [];
        obj["handout_id"] = Id.ToString();
        obj["display_name?"] = DisplayName;
        obj["texture_filename?"] = TextureFilename;
        obj["text?"] = Text;
        obj["visible_to"] = new JsonArray([.. VisibleTo.Select(id => id.ToString())]);
        obj["gm_text?"] = GmText;
        return obj;
    }
}
