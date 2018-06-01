using System;

namespace StringCompactor
{
    public readonly struct StringSpan
    {
        public StringSpan(string str)
            : this(str, 0, str.Length)
        {
        }

        public StringSpan(string original, int start, int length)
        {
            Original = original;
            Start = start;
            Length = length;
        }

        public string Original { get; }

        public int Start { get; }

        public int Length { get; }

        public ReadOnlySpan<char> Span => Original.AsSpan(Start, Length);

        public static StringSpan Empty { get; }

        public static implicit operator StringSpan(string str) => ToStringSpan(str);

        public override string ToString() => Original?.Substring(Start, Length);

        public static StringSpan ToStringSpan(string str) => new StringSpan(str);
    }
}
