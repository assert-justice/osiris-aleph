
using Prion;

namespace Osiris
{
    public abstract class BaseData
    {
        public static bool TryFromNode(PrionNode node, out BaseData data)
        {
            data = null;
            return false;
        }
        public abstract PrionNode ToNode(bool validate = true);
    }
}
