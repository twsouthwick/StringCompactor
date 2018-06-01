using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCompactor
{
    public static class Compactor
    {
        public static IReadOnlyList<StringSpan> Compact(IReadOnlyList<string> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (!input.Any())
            {
                return Array.Empty<StringSpan>();
            }

            var sorted = new SortedSet<string>(input, LengthComparer.Instance);

            StringSpan GetStringSpan(string s)
            {
                if (s == null)
                {
                    return default;
                }

                foreach (var str in sorted)
                {
                    var index = str.IndexOf(s, StringComparison.Ordinal);

                    if (index > -1)
                    {
                        return new StringSpan(str, index, s.Length);
                    }
                }

                throw new InvalidOperationException();
            }

            return input.Select(GetStringSpan).ToList();
        }

        private class LengthComparer : IComparer<string>
        {
            public static LengthComparer Instance { get; } = new LengthComparer();

            private LengthComparer()
            {
            }

            public int Compare(string x, string y)
            {
                if (x == null || y == null)
                {
                    return int.MinValue;
                }

                if (x.Length != y.Length)
                {
                    return y.Length.CompareTo(x.Length);
                }

                return StringComparer.Ordinal.Compare(x, y);
            }
        }
    }
}
