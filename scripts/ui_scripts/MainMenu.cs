using Godot;
using System;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        base._Ready();
        Prion.PrionColor.TryFromHtmlString("#ffff00ff", out Prion.PrionNode node);
        GD.Print(node.ToString());
    }
}
