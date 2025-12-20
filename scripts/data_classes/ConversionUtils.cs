using Godot;
using Prion.Node;

namespace Osiris.DataClass;

public static class ConversionUtils
{
    public static PrionRect2I ToPrionRect2I(Rect2I rect)
    {
        var position = ToPrionVector2I(rect.Position);
        var size = ToPrionVector2I(rect.Size);
        return new(position, size);
    }
    public static Rect2I FromPrionRect2I(PrionRect2I rect)
    {
        var position = FromPrionVector2I(rect.Position);
        var size = FromPrionVector2I(rect.Size);
        return new(position, size);
    }
    public static PrionVector2I ToPrionVector2I(Vector2I vector)
    {
        return new (vector.X, vector.Y);
    }
    public static Vector2I FromPrionVector2I(PrionVector2I vector)
    {
        return new (vector.X, vector.Y);
    }
    public static PrionColor ToPrionColor(Color color)
    {
        if(PrionColor.TryFromHtmlString("#"+color.ToHtml(), out PrionColor prionColor, out string error))
        {
            OsirisSystem.ReportError("Should be unreachable");
            return default;
        }
        return prionColor;
    }
    public static Color FromPrionColor(PrionColor color)
    {
        string htmlString = color.ToHtmlString()[1..];
        return Color.FromHtml(htmlString);
    }
}