using Prion.Node;

namespace Osiris.DataClass;

public interface IToNode<T>
{
    public static abstract bool TryFromNode(PrionNode node, out T data);
}
