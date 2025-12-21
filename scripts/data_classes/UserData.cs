using System;
using Prion.Node;

namespace Osiris.DataClass;

public class UserData(Guid id) : IDataClass<UserData>
{
    public readonly Guid Id = id;
    public string DisplayName = "";
    public string PfpFilename = "";
    public static bool TryFromNode(PrionNode node, out UserData data)
    {
        data = default;
        if(!node.TryAs(out PrionDict dict)) return false;
        if(!dict.TryGet("user_id", out Guid id)) return false;
        data = new(id);
        if(!dict.TryGet("display_name", out data.DisplayName)) return false;
        if(dict.TryGet("pfp_filename?", out string filename)) data.PfpFilename = filename;
        return true;
    }

    public PrionNode ToNode()
    {
        PrionDict dict = new();
        dict.Set("user_id", Id);
        dict.Set("display_name", DisplayName);
        dict.Set("pfp_filename?", PfpFilename);
        return dict;
    }
}
