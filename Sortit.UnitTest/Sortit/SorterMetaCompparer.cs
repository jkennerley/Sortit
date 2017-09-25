using System.Collections.Generic;

namespace SortIt
{
    internal class SorterMetaComparer : IEqualityComparer<SorterMeta>
    {
        public bool Equals(SorterMeta x, SorterMeta y)
        {
            if (x == null || y == null)
                return !(x == null ^ y == null); // If x or y are null, both must be to be equal.
            return x.PropertyName.Equals(y.PropertyName);
        }

        public int GetHashCode(SorterMeta obj)
        {
            return obj.PropertyName.GetHashCode();
        }
    }
}
