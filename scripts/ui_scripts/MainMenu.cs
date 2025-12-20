using System;
using Godot;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        base._Ready();
        Osiris.OsirisSystem.LoadAllSchemas();
        var obj = new Osiris.DataClass.MapData(Guid.NewGuid());
        // var stampImage = new Osiris.DataClass.StampDataImage(Guid.NewGuid());
        // stampImage.ImageFilename = "image.png";
        // obj.Stamps.Add(stampImage);
        // var stampText = new Osiris.DataClass.StampDataText(Guid.NewGuid());
        // stampText.Text = "Hello world!";
        // obj.Stamps.Add(stampText);
        // var stampToken = new Osiris.DataClass.StampDataToken(Guid.NewGuid());
        // stampToken.ActorId = Guid.NewGuid();
        // stampToken.IsUnique = true;
        // stampToken.Stats = new();
        // obj.Stamps.Add(stampToken);
        // obj.ActorId = Guid.NewGuid();
        // obj.IsUnique = true;
        // obj.Stats = new();
        // obj.AutoWrapMode = 2;
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
