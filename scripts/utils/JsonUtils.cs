using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using Godot;

public enum JsonType
{
    Float,
    String,
    Color,
    Guid,
    Bool,
    Array,
    Object,
}

public class JsonValidator(string schema)
{
    readonly JsonNode Schema = JsonNode.Parse(schema);
    readonly List<string> Errors = [];

    public bool IsValid(JsonNode jsonNode)
    {
        Errors.Clear();
        return true;
    }
    public bool HasErrors()
    {
        return Errors.Count > 0;
    }
    public string[] GetErrors()
    {
        return [.. Errors];
    }
}

public static class JsonUtils
{
    public static bool ObjGetBool(JsonObject obj, string propertyName, bool defaultVal)
    {
        if(obj.TryGetPropertyValue(propertyName, out JsonNode node))
        {
            if(node == null) return defaultVal;
            if(node.AsValue().TryGetValue(out bool val))
            {
                return val;
            }
            else
            {
                // Probably should throw an exception.
            }
        }
        return defaultVal;
    }
    public static float ObjGetFloat(JsonObject obj, string propertyName, float defaultVal)
    {
        if(obj.TryGetPropertyValue(propertyName, out JsonNode node))
        {
            if(node == null) return defaultVal;
            if(node.AsValue().TryGetValue(out float val))
            {
                return val;
            }
            else
            {
                // Probably should throw an exception.
            }
        }
        return defaultVal;
    }
    public static int ObjGetInt(JsonObject obj, string propertyName, int defaultVal)
    {
        float val = ObjGetFloat(obj, propertyName, defaultVal);
        // Todo: check if val has a decimal component.
        return (int) val;
    }
    public static string ObjGetString(JsonObject obj, string propertyName, string defaultVal)
    {
        if(obj.TryGetPropertyValue(propertyName, out JsonNode node))
        {
            if(node == null) return defaultVal;
            if(node.AsValue().TryGetValue(out string val))
            {
                return val;
            }
            else
            {
                // Probably should throw an exception.
            }
        }
        return defaultVal;
    }
    public static Color ObjGetColor(JsonObject obj, string propertyName, Color defaultVal)
    {
        string def = defaultVal.ToHtml();
        string val = ObjGetString(obj, propertyName, def);
        if(val == def) return defaultVal;
        return Color.FromString(val, defaultVal);
    }
    public static Color? ObjGetColorNullable(JsonObject obj, string propertyName)
    {
        string val = ObjGetString(obj, propertyName, "");
        if(val == "") return null;
        return Color.FromString(val, Colors.Black);
    }
    // public static bool TryObjGetColor(JsonObject obj, string propertyName, out Color color)
    // {
    //     string val = ObjGetString(obj, propertyName, "");
    //     color = Colors.Black;
    //     return val != "";
    // }
    public static Guid ObjGetGuid(JsonObject obj, string propertyName)
    {
        string val = ObjGetString(obj, propertyName, "");
        if(Guid.TryParse(val, out Guid guid))
        {
            return guid;
        }
        return Guid.NewGuid();
    }
    public static Guid NodeToGuid(JsonNode node)
    {
        if(Guid.TryParse(node.ToString(), out Guid guid))
        {
            return guid;
        }
        return Guid.NewGuid();
    }
    public static float NodeToFloat(JsonNode node, float defaultVal)
    {
        if(node.AsValue().TryGetValue(out float val))
        {
            return val;
        }
        return defaultVal;
    }
    public static Vector2I NodeToVec2I(JsonNode node)
    {
        if(TryParseVec2I(node.ToString(), out Vector2I vec))
        {
            return vec;
        }
        return Vector2I.Zero;
    }
    public static JsonObject ObjGetObj(JsonObject obj, string propertyName)
    {
        if(obj.TryGetPropertyValue(propertyName, out JsonNode node))
        {
            if(node == null) return [];
            if(node.AsValue().TryGetValue(out JsonObject val))
            {
                return val;
            }
            else
            {
                // Probably should throw an exception.
            }
        }
        return [];
    }
    public static JsonNode[] ObjGetArray(JsonObject obj, string propertyName)
    {
        if(obj.TryGetPropertyValue(propertyName, out JsonNode node))
        {
            if(node == null) return [];
            if(node.AsValue().TryGetValue(out JsonArray val))
            {
                return [.. val];
            }
            else
            {
                // Probably should throw an exception.
            }
        }
        return [];
    }
    static bool TryParseVec2I(string str, out Vector2I vector)
    {
        // Todo: validate parse.
        var temp = str.Substr(1, str.Length - 2).Split(',').Select(s => int.Parse(s)).ToArray();
        vector = new(temp[0], temp[1]);
        return true;
    }
    public static Vector2I ObjGetVec2I(JsonObject obj, string propertyName, Vector2I defaultVal)
    {
        string def = defaultVal.ToString();
        string val = ObjGetString(obj, propertyName, def);
        if(val == def) return defaultVal;
        if(TryParseVec2I(val, out Vector2I vector))
        {
            return vector;
        }
        return defaultVal;
    }
    public static int ObjGetEnum(JsonObject obj, string propertyName, string[] matchers, int defaultVal)
    {
        string str = ObjGetString(obj, propertyName, matchers[defaultVal]);
        int idx = Array.IndexOf(matchers, str);
        if(idx == -1) return defaultVal;
        return idx;
    }
    static bool TryParseRect2I(string str, out Rect2I rect)
    {
        // Todo: validate parse.
        var temp = str.Split(',');
        if(TryParseVec2I(temp[0], out Vector2I a)){}
        else{
            rect = new();
            return false;
        }
        if(TryParseVec2I(temp[1], out Vector2I b)){}
        else{
            rect = new();
            return false;
        }
        rect = new(a, b);
        return true;
    }
    public static Rect2I ObjGetRect2I(JsonObject obj, string propertyName, Rect2I defaultVal)
    {
        string def = defaultVal.ToString();
        string val = ObjGetString(obj, propertyName, def);
        if(val == def) return defaultVal;
        if(TryParseRect2I(val, out Rect2I rect))
        {
            return rect;
        }
        return defaultVal;
    }
}