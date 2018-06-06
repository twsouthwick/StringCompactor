﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCompactor
{
    public static class Compactor
    {
        public static IReadOnlyList<StringSegment> Compact(IEnumerable<string> input, StringComparison comparison = StringComparison.Ordinal)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (!input.Any())
            {
                return Array.Empty<StringSegment>();
            }

            var collection = Cache(input);
            var sorted = new SortedSet<string>(collection, new LengthComparer(comparison));

            StringSegment GetStringSpan(string s)
            {
                if (s == null)
                {
                    return default;
                }

                foreach (var str in sorted)
                {
                    var index = str.IndexOf(s, comparison);

                    if (index > -1)
                    {
                        return new StringSegment(str, index, s.Length);
                    }
                }

                throw new InvalidOperationException();
            }

            return collection.Select(GetStringSpan).ToList();
        }

        private static IEnumerable<T> Cache<T>(IEnumerable<T> enumerable)
        {
            if (enumerable is IReadOnlyCollection<T> || enumerable is ICollection<T>)
            {
                return enumerable;
            }
            else
            {
                return enumerable.ToList();
            }
        }

        private class LengthComparer : IComparer<string>
        {
            private readonly StringComparer _comparer;

            public LengthComparer(StringComparison comparison)
            {
                _comparer = StringComparer.FromComparison(comparison);
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

                return _comparer.Compare(x, y);
            }
        }
    }
}
