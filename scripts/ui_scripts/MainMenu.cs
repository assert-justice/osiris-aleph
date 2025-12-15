using Godot;
using Osiris;
using Prion;
using System;
using System.Text.Json.Nodes;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        base._Ready();
        var data = new Actor();
        var node = data.ToNode();
        var str = node.ToJson().ToJsonString();
        GD.Print(str);
        // FileAccess
        // OsirisFileAccess.SetReader(filepath =>
        // {
        //     using var file = FileAccess.Open("res://" + filepath, FileAccess.ModeFlags.Read);
        //     return file.GetAsText();
        // });
        // var logString = OsirisFileAccess.ReadFile("scripts/schemas/asset_log_example.json");
        // // GD.Print(logString);
        // var jsonNode = JsonNode.Parse(logString);
        // // GD.Print(jsonNode.ToJsonString());
        // PrionNode.TryFromJson(jsonNode, out PrionNode prionNode);
        // AssetLog.TryFromNode(prionNode, out AssetLog log);
        // var node = log.ToNode().ToJson().ToJsonString();
        // GD.Print(node);
        // Prion.PrionColor.TryFromHtmlString("#ffff00ff", out Prion.PrionNode node);
        // Prion.PrionNode.TryFromJson(JsonNode.Parse("{\"bite me\": \"poo\"}"), out Prion.PrionNode node);
        // GD.Print(node);
    }
}
