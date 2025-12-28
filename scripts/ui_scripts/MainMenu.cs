using System;
using Godot;
using Osiris.DataClass;
using Osiris.ModuleLoader;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        base._Ready();
        Osiris.OsirisSystem.LoadAllSchemas();
        ModuleLoader.Example();
        // var obj = new ActorData(Guid.NewGuid());
        // var node = obj.ToNode();
        // var json = node.ToJson();
        // var str = json.ToJsonString();
        // GD.Print(str);
    }
}
