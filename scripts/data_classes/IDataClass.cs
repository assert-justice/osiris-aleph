using Prion;

namespace Osiris.DataClass;
public interface IDataClass
{
    public PrionNode ToNode();
    abstract static bool TryFromNodeInternal<T>(PrionNode node, out T data) where T : class, IDataClass;
    public static bool TryFromNode<T>(PrionNode node, out T data) where T : class, IDataClass
    {
        return T.TryFromNodeInternal(node, out data);
    }
}
