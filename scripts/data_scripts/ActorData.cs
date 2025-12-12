
using System;
using System.Linq;
using System.Text.Json.Nodes;

public class ActorData(JsonObject obj)
{
    public Guid Id = RojaUtils.ObjGetGuid(obj, "actor_id");
    public string DisplayName = RojaUtils.ObjGetString(obj, "display_name?", "Mysterious Figure");
    public Guid[] ControlledBy = [.. RojaUtils.ObjGetArray(obj, "controlled_by").Select(o => RojaUtils.NodeToGuid(o))];
    public string PortraitFilename = RojaUtils.ObjGetString(obj, "portrait_filename?", "");
    public string TokenFilename = RojaUtils.ObjGetString(obj, "token_filename?", "");
    public JsonObject Stats = RojaUtils.ObjGetObj(obj, "stats");
    public string Description = RojaUtils.ObjGetString(obj, "description?", "");

    public JsonObject Serialize()
    {
        JsonObject obj = [];
        obj["actor_id"] = Id.ToString();
        obj["display_name?"] = DisplayName;
        obj["controlled_by"] = new JsonArray([.. ControlledBy.Select(id => id.ToString())]);
        obj["portrait_filename?"] = PortraitFilename;
        obj["token_filename?"] = TokenFilename;
        obj["stats"] = Stats;
        obj["description?"] = Description;
        return obj;
    }
}