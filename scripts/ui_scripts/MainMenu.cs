using Godot;
using System;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        base._Ready();
        MapData mapData = new([]);
        GD.Print(mapData.Serialize().ToJsonString());
    }
}
