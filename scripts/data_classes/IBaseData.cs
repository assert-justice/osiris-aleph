using Prion;

namespace Osiris
{
    public interface IBaseData
    {
        public PrionNode ToNode();
        public abstract static bool TryFromNode<T>(PrionNode node, out T data) where T : class, IBaseData;
    }
}
