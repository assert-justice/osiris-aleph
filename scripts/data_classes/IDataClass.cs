using Prion.Node;

namespace Osiris.DataClass;
public interface IDataClass<T> where T : class
{
    public static abstract bool TryFromNode(PrionNode node, out T data);
    public PrionNode ToNode();
}
