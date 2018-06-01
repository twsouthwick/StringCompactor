using System;
using Xunit;

namespace StringCompactor
{
    public class StringSpanTests
    {
        [Fact]
        public void DefaultStringSpan()
        {
            var span = default(StringSpan);

            Assert.Null(span.ToString());
            Assert.Null(span.Original);
            Assert.Equal(0, span.Start);
            Assert.Equal(0, span.Length);
        }

        [Fact]
        public void EmptySpan()
        {
            Assert.Equal(default, StringSpan.Empty);
        }

        [Fact]
        public void ToStringSpan()
        {
            const string Str = "hello world";
            Assert.Equal(new StringSpan(Str), StringSpan.ToStringSpan(Str));
        }

        [Fact]
        public void ImplicitConverter()
        {
            const string Str = "hello world";
            Assert.Equal(new StringSpan(Str), Str);
        }

        [InlineData("hello2", 0, 2, "he")]
        [InlineData("hello2", 0, 5, "hello")]
        [Theory]
        public void Substring(string original, int start, int length, string expected)
        {
            Assert.Equal(expected, new StringSpan(original, start, length).ToString());
            Assert.Equal(expected.AsSpan().ToArray(), new StringSpan(original, start, length).Span.ToArray());
        }
    }
}
