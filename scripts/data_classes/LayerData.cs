using System.Collections.Generic;
using System.Linq;
using Prion.Node;

namespace Osiris.DataClass;

public class LayerData : IDataClass<LayerData>
{
    public string DisplayName = "[Unnamed Layer]";
    public bool IsVisible = false;
    public List<StampData> Stamps = [];
    public static bool TryFromNode(PrionNode node, out LayerData data)
    {
        data = default;
        if(!node.TryAs(out PrionDict dict)) return false;
        data = new();
        if(!dict.TryGet("display_name", out data.DisplayName)) return false;
        if(!dict.TryGet("is_visible", out data.IsVisible)) return false;
        if(!dict.TryGet("stamps", out PrionArray prionArray)) return false;
        foreach (var item in prionArray.Value)
        {
            if(!StampData.TryFromNode(item, out StampData stamp)) return false;
            data.Stamps.Add(stamp);
        }
        return true;
    }
    public PrionNode ToNode()
    {
        PrionDict dict = new();
        dict.Set("display_name", DisplayName);
        dict.Set("is_visible", IsVisible);
        PrionArray stamps = new([..Stamps.Select(s => s.ToNode())]);
        dict.Set("stamps", stamps);
        return dict;
    }
}
