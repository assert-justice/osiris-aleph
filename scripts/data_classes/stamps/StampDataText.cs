using System;
using Prion.Node;

namespace Osiris.DataClass;

public class StampDataText(Guid id) : StampData(id)
{
    public override PrionNode ToNode()
    {
        var node = BaseToNode();
        return node;
    }

    public override bool TryFinishFromNode(PrionDict prionDict)
    {
        throw new NotImplementedException();
    }
}
