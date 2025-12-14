using Godot;
using System;
using System.Text.Json.Nodes;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        base._Ready();
        // Prion.PrionColor.TryFromHtmlString("#ffff00ff", out Prion.PrionNode node);
        Prion.PrionNode.TryFromJson(JsonNode.Parse("{\"bite me\": \"poo\"}"), out Prion.PrionNode node);
        GD.Print(node.ToString());
    }
}
