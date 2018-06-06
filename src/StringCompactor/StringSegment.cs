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
            Original = original;
            Start = start;
            Length = length;
        }

        public string Original { get; }

        public int Start { get; }

        public int Length { get; }

        public ReadOnlySpan<char> Span => Original.AsSpan(Start, Length);

        public static StringSegment Empty { get; }

        public static implicit operator StringSegment(string str) => ToStringSpan(str);

        public override string ToString() => Original?.Substring(Start, Length);

        public static StringSegment ToStringSpan(string str) => new StringSegment(str);

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
