
using System.Text.Json.Nodes;
using Godot;

public enum AuraShape
{
    Circle,
    Square,
}

public class AuraData(JsonObject obj)
{
    static string[] matchers = ["circle", "square"];
    public Color? FillColor = JsonUtils.ObjGetColorNullable(obj, "fill_color?");
    public Color? OutlineColor = JsonUtils.ObjGetColorNullable(obj, "outline_color?");
    public AuraShape Shape = (AuraShape)JsonUtils.ObjGetEnum(obj, "shape", matchers, 0);
    public string Name = JsonUtils.ObjGetString(obj, "display_name?", "");
    public AuraProperties Properties = new(JsonUtils.ObjGetObj(obj, "properties"));

    public JsonObject Serialize()
    {
        JsonObject obj = [];
        if(FillColor is Color fillColor)
        {
            obj["fill_color"] = fillColor.ToHtml();
        }
        if(OutlineColor is Color outlineColor)
        {
            obj["outline_color"] = outlineColor.ToHtml();
        }
        obj["shape"] = matchers[(int)Shape];
        obj["display_name"] = Name;
        obj["aura_properties"] = Properties.Serialize();
        return obj;
    }
}
