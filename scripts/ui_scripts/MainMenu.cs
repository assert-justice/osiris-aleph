using Godot;
using Osiris.Vm;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        base._Ready();
        Vm.Example();
    }
}
