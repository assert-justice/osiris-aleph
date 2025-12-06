
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
        StampType stampType = (StampType)JsonUtils.ObjGetEnum(obj, "type", StampTypeLookup, 0);
        switch (stampType)
        {
            case StampType.Text:
                return new StampDataText(obj, stampType);
            case StampType.Image:
                return new StampDataImage(obj, stampType);
            case StampType.Token:
                return new StampDataToken(obj, stampType);
            default:
            // Should probably throw an exception or something.
            break;
        }
        return new StampDataText(obj, StampType.Text);
    }
}