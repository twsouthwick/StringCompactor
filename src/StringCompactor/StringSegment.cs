using System;

namespace StringCompactor
{
    public readonly struct StringSegment
    {
        public StringSegment(string str)
            : this(str, 0, str.Length)
        {
        }

        public StringSegment(string original, int start, int length)
        {
            Buffer = original;
            Start = start;
            Length = length;
        }

        public string Buffer { get; }

        public int Start { get; }

        public int Length { get; }

        public string Value => Buffer?.Substring(Start, Length);

        public ReadOnlySpan<char> Span => Buffer.AsSpan(Start, Length);

        public static StringSegment Empty { get; }

        public static implicit operator StringSegment(string str) => ToStringSpan(str);

        public override string ToString() => Value;

        public static StringSegment ToStringSpan(string str) => new StringSegment(str);
    }
}
