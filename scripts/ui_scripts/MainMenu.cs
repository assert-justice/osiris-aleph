using System;
using Godot;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        base._Ready();
        Osiris.OsirisSystem.LoadAllSchemas();
        var obj = new Osiris.DataClass.StampDataImage(Guid.NewGuid());
        obj.StretchMode = 1;
        obj.ImageFilename = "image.png";
        var prionNode = obj.ToNode();
        var jsonNode = prionNode.ToJson();
        GD.Print(jsonNode.ToJsonString());
        // var obj = new Prion.Node.PrionString("hello world");
        // var str = obj.ToString();
        // var json = obj.ToJson();
        // GD.Print(json);
    }
}
