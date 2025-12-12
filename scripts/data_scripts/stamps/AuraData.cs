
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
    public Color? FillColor = RojaUtils.ObjGetColorNullable(obj, "fill_color?");
    public Color? OutlineColor = RojaUtils.ObjGetColorNullable(obj, "outline_color?");
    public AuraShape Shape = (AuraShape)RojaUtils.ObjGetEnum(obj, "shape", matchers, 0);
    public string Name = RojaUtils.ObjGetString(obj, "display_name?", "");

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
        return obj;
    }
}
