using Prion.Node;

namespace Osiris.DataClass;

public class StampDataImage : StampData
{
    public override PrionNode ToNode()
    {
        var node = BaseToNode();
        return node;
    }
}
