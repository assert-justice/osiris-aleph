using Godot;
using System;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        base._Ready();
        Prion.PrionUBigInt.TryFromHexString("0xff", out Prion.PrionNode node);
        foreach (var item in (node as Prion.PrionUBigInt).Data)
        {
            GD.Print(item);
        }
        GD.Print(node.ToString());
        // var data = new MapData([]);
        // GD.Print(data.Serialize().ToJsonString());
    }
}
