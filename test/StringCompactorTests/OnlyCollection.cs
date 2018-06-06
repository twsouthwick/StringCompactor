using System.Collections;
using System.Collections.Generic;

namespace StringCompactor
{
    internal class OnlyCollection<T> : ICollection<T>
    {
        private readonly ICollection<T> _other;

        public OnlyCollection(ICollection<T> other)
        {
            _other = other;
        }

        public int Count => _other.Count;

        public bool IsReadOnly => _other.IsReadOnly;

        public void Add(T item)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex) => _other.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => _other.GetEnumerator();

        public bool Remove(T item)
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
