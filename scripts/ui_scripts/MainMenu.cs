using System;
using Godot;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        base._Ready();
        Osiris.OsirisSystem.LoadAllSchemas();
        var obj = new Osiris.DataClass.StampDataText(Guid.NewGuid());
        obj.AutoWrapMode = 2;
        // obj.StretchMode = 1;
        // obj.ImageFilename = "image.png";
        var prionNode = obj.ToNode();
        var jsonNode = prionNode.ToJson();
        GD.Print(jsonNode.ToJsonString());
        // var obj = new Prion.Node.PrionString("hello world");
        // var str = obj.ToString();
        // var json = obj.ToJson();
        // GD.Print(json);
    }
}
