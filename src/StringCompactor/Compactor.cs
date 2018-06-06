using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StringCompactor
{
    public static class Compactor
    {
        public static IReadOnlyList<StringSegment> Compact(IEnumerable<string> input, StringComparison comparison = StringComparison.Ordinal, IProgress progress = null)
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

            StringSegment GetStringSpan(string s, int i)
            {
                progress?.ProgressChanged(i, collection.Length);

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

            return collection.AsParallel().Select(GetStringSpan).ToList();
        }

        private static EnumerableWithLength<T> Cache<T>(IEnumerable<T> enumerable)
        {
            if (enumerable is IReadOnlyCollection<T> readOnly)
            {
                return new EnumerableWithLength<T>(readOnly);
            }
            else if (enumerable is ICollection<T> collection)
            {
                return new EnumerableWithLength<T>(collection);
            }
            else
            {
                return new EnumerableWithLength<T>((ICollection<T>)enumerable.ToList());
            }
        }

        private class EnumerableWithLength<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> _items;

            public EnumerableWithLength(IReadOnlyCollection<T> items)
            {
                _items = items;
                Length = items.Count;
            }

            public EnumerableWithLength(ICollection<T> items)
            {
                _items = items;
                Length = items.Count;
            }

            public int Length { get; }

            public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();
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
