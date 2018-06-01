using System;
using System.Linq;
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
            Assert.Same(Array.Empty<StringSpan>(), Compactor.Compact(Array.Empty<string>()));
        }

        [Fact]
        public void DoubleDifferentCase()
        {
            const string String1 = "hello";
            const string String2 = "Hello";

            var compacted = Compactor.Compact(new[] { String1, String2 }, StringComparison.OrdinalIgnoreCase);

            Assert.Equal(2, compacted.Count);

            Assert.Same(String1, compacted[0].Original);
            Assert.Equal(new StringSpan(String1), compacted[0]);

            Assert.Same(String1, compacted[1].Original);
            Assert.Equal(new StringSpan(String1), compacted[1]);
        }

        [Fact]
        public void SingleItem()
        {
            const string Str = "hello";
            var compacted = Compactor.Compact(new[] { Str });

            var single = Assert.Single(compacted);

            Assert.Same(Str, single.Original);
            Assert.Equal(new StringSpan(Str), single);
        }

        [Fact]
        public void DoubleUnique()
        {
            const string String1 = "hello";
            const string String2 = "world";

            var compacted = Compactor.Compact(new[] { String1, String2 });

            Assert.Equal(2, compacted.Count);

            Assert.Same(String1, compacted[0].Original);
            Assert.Equal(new StringSpan(String1), compacted[0]);

            Assert.Same(String2, compacted[1].Original);
            Assert.Equal(new StringSpan(String2), compacted[1]);
        }

        [Fact]
        public void DoubleSubstring()
        {
            const string String1 = "hello1";
            const string String2 = "hello";

            var compacted = Compactor.Compact(new[] { String1, String2 });

            Assert.Equal(2, compacted.Count);

            Assert.Same(String1, compacted[0].Original);
            Assert.Equal(new StringSpan(String1), compacted[0]);

            Assert.Same(String1, compacted[1].Original);
            Assert.Equal(new StringSpan(String1, 0, String2.Length), compacted[1]);
        }

        [Fact]
        public void TripleSubstringWithNull()
        {
            const string String1 = "hello1";
            const string String2 = "hello";

            var compacted = Compactor.Compact(new[] { String1, String2, null });

            Assert.Equal(3, compacted.Count);

            Assert.Same(String1, compacted[0].Original);
            Assert.Equal(new StringSpan(String1), compacted[0]);

            Assert.Same(String1, compacted[1].Original);
            Assert.Equal(new StringSpan(String1, 0, String2.Length), compacted[1]);

            Assert.Null(compacted[2].Original);
            Assert.Null(compacted[2].ToString());
            Assert.Equal(StringSpan.Empty, compacted[2]);
        }
    }
}
