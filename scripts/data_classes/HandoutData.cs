using System;
using System.Collections.Generic;
using System.Linq;
using Prion;
using Prion.Node;

namespace Osiris.DataClass;
public class HandoutData(Guid guid) : IDataClass<HandoutData>
{
    public readonly Guid Id = guid;
    public string DisplayName = "[Mysterious Note]";
    public string ImageFilename = "";
    public string Text = "";
    public HashSet<Guid> VisibleTo = [];
    public HashSet<Guid> OwnedBy = [];
    public string GMNotes = "";

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
        if(!prionDict.TryGet("owned_by", out data.OwnedBy)) return false;
        return true;
    }
    public PrionNode ToNode()
    {
        PrionDict prionDict = new();
        prionDict.Set("handout_id", Id);
        prionDict.Set("display_name?", DisplayName);
        prionDict.Set("image_filename?", ImageFilename);
        prionDict.Set("text?", Text);
        prionDict.Set("visible_to", VisibleTo);
        prionDict.Set("owned_by", OwnedBy);
        prionDict.Set("gm_notes?", GMNotes);
        return prionDict;
    }
}
