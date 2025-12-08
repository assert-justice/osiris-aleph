using Godot;
using System;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        base._Ready();
        var data = new ActorData([]);
        GD.Print(data.Serialize().ToJsonString());
    }
}
