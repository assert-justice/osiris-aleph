using System.Text.Json.Nodes;
using Godot;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        base._Ready();
        Osiris.OsirisSystem.LoadAllSchemas();
        // var blocker = new Osiris.DataClass.BlockerData();
        // blocker.End = Vector2I.Right;
        // var prionNode = blocker.ToNode();
        // var jsonNode = prionNode.ToJson();
        // GD.Print(jsonNode.ToJsonString());
    }
}
