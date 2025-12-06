
using System;
using System.Linq;
using System.Text.Json.Nodes;

public class ActorData(JsonObject obj)
{
    public Guid Id = JsonUtils.ObjGetGuid(obj, "actor_id");
    public string DisplayName = JsonUtils.ObjGetString(obj, "display_name?", "Mysterious Figure");
    public Guid[] ControlledBy = [.. JsonUtils.ObjGetArray(obj, "controlled_by").Select(o => JsonUtils.NodeToGuid(o))];
    public string PortraitFilename = JsonUtils.ObjGetString(obj, "portrait_filename?", "");
    public string TokenFilename = JsonUtils.ObjGetString(obj, "token_filename?", "");
    public JsonObject Stats = JsonUtils.ObjGetObj(obj, "stats");
    public string Description = JsonUtils.ObjGetString(obj, "description?", "");

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