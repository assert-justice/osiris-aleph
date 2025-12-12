
using System.Text.Json.Nodes;

public enum StampType
{
    Text,
    Image,
    Token,
}

public static class StampDataParser
{
    static readonly string[] StampTypeLookup = ["text", "image", "token"];
    public static StampData Parse(JsonObject obj)
    {
        StampType stampType = (StampType)RojaUtils.ObjGetEnum(obj, "type", StampTypeLookup, 0);
        switch (stampType)
        {
            case StampType.Text:
                return new StampDataText(obj);
            case StampType.Image:
                return new StampDataImage(obj);
            case StampType.Token:
                return new StampDataToken(obj);
            default:
            // Should probably throw an exception or something.
            break;
        }
        return new StampDataText(obj);
    }
}