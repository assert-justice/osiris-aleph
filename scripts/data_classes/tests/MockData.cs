using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Godot;
using Prion.Node;

namespace Osiris.DataClass.Tests;

public static class MockData
{
    public const string ValidIdentChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
    public static readonly Random Rng = new();
    static readonly StringBuilder Builder = new();
    public static string GetRandomColorString()
    {
        const string hexChars = "0123456789abcdefABCDEF";
        int[] lens = [3, 4, 6, 8];
        int length = lens[Rng.Next(4)];
        Builder.EnsureCapacity(length);
        for (int idx = 0; idx < length; idx++)
        {
            Builder.Append(hexChars[Rng.Next(hexChars.Length)]);
        }
        return Builder.ToString();
    }
    public static string GetRandomIdent()
    {
        int length = Rng.Next(1,256);
        return GetRandomIdent(length);
    }
    public static string GetRandomIdent(int length)
    {
        Builder.EnsureCapacity(length);
        for (int f = 0; f < length; f++)
        {
            // char c = ValidIdentChars[Rng.Next(ValidIdentChars.Length)];
            Builder.Append(GetRandomElement(ValidIdentChars));
        }
        return Builder.ToString();
    }
    public static string GetRandomText(int length)
    {
        // TODO: expand to all ascii printable characters, eventually utf8
        Builder.EnsureCapacity(length);
        for (int f = 0; f < length; f++)
        {
            // char c = ValidIdentChars[Rng.Next(ValidIdentChars.Length)];
            Builder.Append(GetRandomElement(ValidIdentChars));
        }
        return Builder.ToString();
    }
    public static T GetRandomElement<T>(IEnumerable<T> elements)
    {
        int idx = Rng.Next(elements.Count());
        return elements.ElementAt(idx);
    }
    public static U GetRandomCollection<T,U>(Func<T> factory, int numElements) where U : class, ICollection<T>, new()
    {
        return [..Enumerable.Range(0, numElements).Select(_ => factory())];
    }
    public static HashSet<T> GetRandomSet<T>(Func<T> factory, int numElements)
    {
        return GetRandomCollection<T, HashSet<T>>(factory, numElements);
    }
    public static string GetRandomHexColor()
    {
        return "0x" + GetRandomColorString();
    }
    public static string GetRandomHtmlColor()
    {
        return "#" + GetRandomColorString();
    }
    public static string ExpandColor(string s)
    {
        s = s.ToLower();
        string prefix = "";
        string value = s;
        if (s.StartsWith("0x"))
        {
            prefix = "0x";
            value = s[2..];
        }
        else if(s.StartsWith('#'))
        {
            prefix = "#";
            value = s[1..];
        }
        string expanded = "";
        switch (value.Length)
        {
            case 3:
            for (int idx = 0; idx < value.Length; idx++)
            {
                expanded += value[idx];
                expanded += value[idx];
            }
            expanded += "ff";
            break;
            case 4:
            for (int idx = 0; idx < value.Length; idx++)
            {
                expanded += value[idx];
                expanded += value[idx];
            }
            break;
            case 6:
            expanded = value + "ff";
            break;
            case 8:
            expanded = value;
            break;
            default:
            break;
        }
        return prefix + expanded;
    }
    public static Vector2I GetRandomVector2I(int minX, int maxX, int minY, int maxY)
    {
        return new(Rng.Next(minX, maxX), Rng.Next(minY, maxY));
    }
    public static bool GetRandomBool()
    {
        return Rng.Next(2) == 0;
    }
}
