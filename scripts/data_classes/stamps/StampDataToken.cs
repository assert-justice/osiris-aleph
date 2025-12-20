using Prion.Node;

namespace Osiris.DataClass;

public class StampDataToken : StampData
{
    public override PrionNode ToNode()
    {
        var node = BaseToNode();
        return node;
    }
}
