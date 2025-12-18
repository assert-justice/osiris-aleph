using System.Text.Json.Nodes;
using Godot;
using Osiris.DataClass;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        base._Ready();
        // Osiris.OsirisSystem.LoadAllSchemas();
        // var blocker = new Osiris.DataClass.BlockerData();
        // blocker.End = Vector2I.Right;
        var obj = new TileGroupData();
        obj.Bitfield.SetBit(0, true);
        obj.Tiles.Add(Vector2I.Zero);
        obj.Tiles.Add(Vector2I.One);
        var prionNode = obj.ToNode();
        var jsonNode = prionNode.ToJson();
        GD.Print(jsonNode.ToJsonString());
    }
}
