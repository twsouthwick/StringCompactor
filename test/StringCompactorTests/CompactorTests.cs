using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using Xunit;

namespace StringCompactor
{
    public class CompactorTests
    {
        [Fact]
        public void NullInput()
        {
            Assert.Throws<ArgumentNullException>(() => Compactor.Compact(null));
        }

        [Fact]
        public void ReturnsEmpty()
        {
            Assert.Same(Array.Empty<StringSegment>(), Compactor.Compact(Array.Empty<string>()));
        }

        [Fact]
        public void DoubleDifferentCase()
        {
            const string String1 = "hello";
            const string String2 = "Hello";

            var compacted = Compactor.Compact(new[] { String1, String2 }, StringComparison.OrdinalIgnoreCase);

            Assert.Equal(2, compacted.Count);

            Assert.Same(String1, compacted[0].Buffer);
            Assert.Equal(new StringSegment(String1), compacted[0]);

            Assert.Same(String1, compacted[1].Buffer);
            Assert.Equal(new StringSegment(String1), compacted[1]);
        }

        [Fact]
        public void SingleItem()
        {
            const string Str = "hello";
            var compacted = Compactor.Compact(new[] { Str });

            var single = Assert.Single(compacted);

            Assert.Same(Str, single.Buffer);
            Assert.Equal(new StringSegment(Str), single);
        }

        [Fact]
        public void DoubleUnique()
        {
            const string String1 = "hello";
            const string String2 = "world";

            var compacted = Compactor.Compact(new[] { String1, String2 });

            Assert.Equal(2, compacted.Count);

            Assert.Same(String1, compacted[0].Buffer);
            Assert.Equal(new StringSegment(String1), compacted[0]);

            Assert.Same(String2, compacted[1].Buffer);
            Assert.Equal(new StringSegment(String2), compacted[1]);
        }

        [Fact]
        public void DoubleSubstring()
        {
            const string String1 = "hello1";
            const string String2 = "hello";

            var compacted = Compactor.Compact(new[] { String1, String2 });

            Assert.Equal(2, compacted.Count);

            Assert.Same(String1, compacted[0].Buffer);
            Assert.Equal(new StringSegment(String1), compacted[0]);

            Assert.Same(String1, compacted[1].Buffer);
            Assert.Equal(new StringSegment(String1, 0, String2.Length), compacted[1]);
        }

        [Fact]
        public void DoubleSubstringWithCustomCollection()
        {
            const string String1 = "hello1";
            const string String2 = "hello";

            var compacted = Compactor.Compact(new OnlyCollection<string>(new[] { String1, String2 }));

            Assert.Equal(2, compacted.Count);

            Assert.Same(String1, compacted[0].Buffer);
            Assert.Equal(new StringSegment(String1), compacted[0]);

            Assert.Same(String1, compacted[1].Buffer);
            Assert.Equal(new StringSegment(String1, 0, String2.Length), compacted[1]);
        }

        [Fact]
        public void DoubleSubstringWithEnumerable()
        {
            const string String1 = "hello1";
            const string String2 = "hello";

            int count = 0;

            IEnumerable<string> GetList()
            {
                yield return String1;
                yield return String2;
                count++;
            }

            var compacted = Compactor.Compact(GetList());

            Assert.Equal(1, count);

            Assert.Equal(2, compacted.Count);

            Assert.Same(String1, compacted[0].Buffer);
            Assert.Equal(new StringSegment(String1), compacted[0]);

            Assert.Same(String1, compacted[1].Buffer);
            Assert.Equal(new StringSegment(String1, 0, String2.Length), compacted[1]);
        }

        [Fact]
        public void TripleSubstringWithNull()
        {
            const string String1 = "hello1";
            const string String2 = "hello";

            var compacted = Compactor.Compact(new[] { String1, String2, null });

            Assert.Equal(3, compacted.Count);

            Assert.Same(String1, compacted[0].Buffer);
            Assert.Equal(new StringSegment(String1), compacted[0]);

            Assert.Same(String1, compacted[1].Buffer);
            Assert.Equal(new StringSegment(String1, 0, String2.Length), compacted[1]);

            Assert.Null(compacted[2].Buffer);
            Assert.Null(compacted[2].ToString());
            Assert.Equal(StringSegment.Empty, compacted[2]);
        }
    }
}
