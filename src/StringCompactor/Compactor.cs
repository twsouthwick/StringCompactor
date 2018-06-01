using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCompactor
{
    public static class Compactor
    {
        public static IReadOnlyCollection<StringSpan> Compact(IEnumerable<string> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (!input.Any())
            {
                return Array.Empty<StringSpan>();
            }

            return input.Select(i => new StringSpan(i)).ToList();
        }
    }
}
