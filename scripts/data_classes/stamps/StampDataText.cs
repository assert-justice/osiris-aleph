using Prion.Node;

namespace Osiris.DataClass;

public class StampDataText : StampData
{
    public override PrionNode ToNode()
    {
        var node = BaseToNode();
        return node;
    }
}
