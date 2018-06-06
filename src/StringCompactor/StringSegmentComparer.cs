using System;
using System.Collections.Generic;

namespace StringCompactor
{
    public class StringSegmentComparer : IEqualityComparer<StringSegment>, IComparer<StringSegment>
    {
        public static StringSegmentComparer Ordinal { get; } = new StringSegmentComparer(StringComparison.Ordinal);

        private readonly StringComparison _comparison;
        private readonly StringComparer _comparer;

        private StringSegmentComparer(StringComparison comparison)
        {
            _comparison = comparison;
            _comparer = StringComparer.FromComparison(comparison);
        }

        public int Compare(StringSegment x, StringSegment y)
        {
            var minLength = Math.Min(x.Length, y.Length);
            var diff = string.Compare(x.Buffer, x.Start, y.Buffer, y.Start, minLength, _comparison);

            if (diff == 0)
            {
                diff = x.Length - y.Length;
            }

            return diff;
        }

        public bool Equals(StringSegment x, StringSegment y)
        {
            return Compare(x, y) == 0;
        }

        public int GetHashCode(StringSegment obj)
        {
            var hashCode = new HashCode();

            hashCode.Add(obj.Value, _comparer);
            hashCode.Add(obj.Start);
            hashCode.Add(obj.Length);

            return hashCode.ToHashCode();
        }
    }
}
