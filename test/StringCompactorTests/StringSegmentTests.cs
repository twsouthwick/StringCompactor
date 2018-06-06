using System;
using Xunit;

namespace StringCompactor
{
    public class StringSegmentTests
    {
        [Fact]
        public void DefaultStringSegment()
        {
            var span = default(StringSegment);

            Assert.Null(span.ToString());
            Assert.Null(span.Original);
            Assert.Equal(0, span.Start);
            Assert.Equal(0, span.Length);
        }

        [Fact]
        public void EmptySegment()
        {
            Assert.Equal(default, StringSegment.Empty);
        }

        [Fact]
        public void ToString()
        {
            const string Str = "hello world";
            Assert.Equal(new StringSegment(Str), StringSegment.ToStringSpan(Str));
        }

        [Fact]
        public void ImplicitConverter()
        {
            const string Str = "hello world";
            Assert.Equal(new StringSegment(Str), Str);
        }

        [InlineData("hello2", 0, 2, "he")]
        [InlineData("hello2", 0, 5, "hello")]
        [Theory]
        public void Substring(string original, int start, int length, string expected)
        {
            Assert.Equal(expected, new StringSegment(original, start, length).ToString());
            Assert.Equal(expected.AsSpan().ToArray(), new StringSegment(original, start, length).Span.ToArray());
        }
    }
}
