using System;
using Godot;
using Osiris.ModuleLoader;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        base._Ready();
        Osiris.OsirisSystem.LoadAllSchemas();
        ModuleLoader.Example();
    }
}
