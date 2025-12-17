using Prion;

namespace Osiris
{
    public interface IBaseData
    {
        public PrionNode ToNode();
        abstract static bool TryFromNodeInternal<T>(PrionNode node, out T data) where T : class, IBaseData;
        public static bool TryFromNode<T>(PrionNode node, out T data) where T : class, IBaseData
        {
            return T.TryFromNodeInternal(node, out data);
        }
    }
}
