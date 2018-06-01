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
            Assert.Same(Array.Empty<StringSpan>(), Compactor.Compact(Enumerable.Empty<string>()));
            Assert.Same(Array.Empty<StringSpan>(), Compactor.Compact(Array.Empty<string>()));
        }

        [Fact]
        public void SingleItem()
        {
            const string Str = "hello";
            var compacted = Compactor.Compact(new[] { Str });

            var single = Assert.Single(compacted);

            Assert.Same(Str, single.Original);
            Assert.Equal(0, single.Start);
            Assert.Equal(Str.Length, single.Length);
        }
    }
}
