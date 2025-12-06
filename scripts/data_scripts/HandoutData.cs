
using System;
using System.Linq;
using System.Text.Json.Nodes;

public class HandoutData(JsonObject obj)
{
    public Guid Id = JsonUtils.ObjGetGuid(obj, "handout_id");
    public string DisplayName = JsonUtils.ObjGetString(obj, "display_name?", "Mysterious Note");
    public string TextureFilename = JsonUtils.ObjGetString(obj, "texture_filename?", "");
    public string Text = JsonUtils.ObjGetString(obj, "text?", "");
    public Guid[] VisibleTo = [.. JsonUtils.ObjGetArray(obj, "visible_to").Select(o => JsonUtils.NodeToGuid(o))];
    public string GmText = JsonUtils.ObjGetString(obj, "gm_text?", "");

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
