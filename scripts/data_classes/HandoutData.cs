using System;
using System.Collections.Generic;
using System.Linq;
using Prion;
using Prion.Node;

namespace Osiris.DataClass;
public class HandoutData : IDataClass<HandoutData>
{
    public readonly Guid Id;
    public string DisplayName = "[Mysterious Note]";
    public string ImageFilename = "";
    public string Text = "";
    public HashSet<Guid> VisibleTo = [];
    public HashSet<Guid> Owners = [];
    public string GMNotes = "";

    public HandoutData()
    {
        Id = Guid.NewGuid();
    }
    public HandoutData(Guid guid)
    {
        Id = guid;
    }

    public static bool TryFromNode(PrionNode node, out HandoutData data)
    {
        data = default;
        if(!node.TryAs(out PrionDict prionDict)) return false;
        if(!prionDict.TryGet("handout_id", out Guid guid)) return false;
        data = new(guid)
        {
            DisplayName = prionDict.GetDefault("display_name?", "[Mysterious Note]"),
            ImageFilename = prionDict.GetDefault("image_filename?", ""),
            Text = prionDict.GetDefault("text?", ""),
            GMNotes = prionDict.GetDefault("gm_notes?", ""),
        };
        if(!prionDict.TryGet("visible_to", out data.VisibleTo)) return false;
        if(!prionDict.TryGet("owners", out data.Owners)) return false;
        return true;
    }
    public PrionNode ToNode()
    {
        PrionDict prionDict = new();
        prionDict.Set("handout_id", Id);
        prionDict.Set("display_name?", DisplayName);
        prionDict.Set("image_filename?", ImageFilename);
        prionDict.Set("text?", Text);
        prionDict.Value["visible_to"] = new PrionArray([.. VisibleTo.Select(o => new PrionGuid(o))]);
        prionDict.Value["owners"] = new PrionArray([.. Owners.Select(o => new PrionGuid(o))]);
        prionDict.Set("gm_notes?", GMNotes);
        return prionDict;
    }
}
