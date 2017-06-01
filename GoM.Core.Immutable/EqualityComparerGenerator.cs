using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core.Immutable
{
    public static class EqualityComparerGenerator
    {
        public static IEqualityComparer<T> CreateEqualityComparer<T>(Func<T, T, bool> isEqual, Func<T, int> getHashcode)
        {
            if(isEqual == null) throw new ArgumentNullException(nameof(isEqual));
            if(getHashcode == null) throw new ArgumentNullException(nameof(getHashcode));
            return new CustomComparer<T>(isEqual, getHashcode);
        }
    }

    class CustomComparer<T> : IEqualityComparer<T>
    {
        private Func<T, T, bool> _isEqual;
        private Func<T, int> _getHashCode;

        public CustomComparer(Func<T, T, bool> isEqual, Func<T, int> getHashcode)
        {
            _isEqual = isEqual;
            _getHashCode = getHashcode;
        }

        public bool Equals(T x, T y) { return _isEqual(x, y); }

        public int GetHashCode(T obj) { return _getHashCode(obj); }
    }
}
