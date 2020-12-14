namespace Indexers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    /// <inheritdoc cref="IMap2D{TKey1,TKey2,TValue}" />
    public class Map2D<TKey1, TKey2, TValue> : IMap2D<TKey1, TKey2, TValue>
    {
        private Dictionary<Tuple<TKey1, TKey2>, TValue> map = new Dictionary<Tuple<TKey1, TKey2>, TValue>();

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.NumberOfElements" />
        public int NumberOfElements
        {
            get { return this.map.Count; }
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.this" />
        public TValue this[TKey1 key1, TKey2 key2]
        {
            get { return this.map.GetValueOrDefault(Tuple.Create(key1, key2)); }
            set { this.map.Add(Tuple.Create(key1, key2), value); }
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetRow(TKey1)" />
        public IList<Tuple<TKey2, TValue>> GetRow(TKey1 key1)
        {
            List<Tuple<TKey2, TValue>> tuples = new List<Tuple<TKey2, TValue>>();
            foreach(var t in this.map)
            {
                if (t.Key.Item1.Equals(key1))
                {
                    tuples.Add(Tuple.Create(t.Key.Item2, t.Value));
                }
            }
            return tuples;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetColumn(TKey2)" />
        public IList<Tuple<TKey1, TValue>> GetColumn(TKey2 key2)
        {
            List<Tuple<TKey1, TValue>> tuples = new List<Tuple<TKey1, TValue>>();
            foreach (var t in this.map)
            {
                if (t.Key.Item2.Equals(key2))
                {
                    tuples.Add(Tuple.Create(t.Key.Item1, t.Value));
                }
            }
            return tuples;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetElements" />
        public IList<Tuple<TKey1, TKey2, TValue>> GetElements()
        {
            List<Tuple<TKey1, TKey2, TValue>> tuples = new List<Tuple<TKey1, TKey2, TValue>>();
            foreach (var t in this.map)
            { 
                tuples.Add(Tuple.Create(t.Key.Item1, t.Key.Item2, t.Value));
            }
            return tuples;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.Fill(IEnumerable{TKey1}, IEnumerable{TKey2}, Func{TKey1, TKey2, TValue})" />
        public void Fill(IEnumerable<TKey1> keys1, IEnumerable<TKey2> keys2, Func<TKey1, TKey2, TValue> generator)
        {
            foreach(TKey1 key1 in keys1)
            {
                foreach(TKey2 key2 in keys2)
                {
                    this.map.Add(Tuple.Create(key1, key2), generator(key1, key2));
                }
            }
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)" />
        public bool Equals(IMap2D<TKey1, TKey2, TValue> other)
        {
            if (other is Map2D<TKey1, TKey2, TValue> other2)
            {
                return this.Equals(other2);
            }
            return false;
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj)
        {
            return obj is Map2D<TKey1, TKey2, TValue> d &&
                   EqualityComparer<Dictionary<Tuple<TKey1, TKey2>, TValue>>.Default.Equals(map, d.map);
        }



        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
            return this.map != null ? this.map.GetHashCode() : 0;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.ToString"/>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(Tuple<TKey1,TKey2,TValue> t in this.GetElements())
            {
                sb.Append($"k1: {t.Item1}, k2: {t.Item2}, value: {t.Item3}");
            }
            return sb.ToString();
        }
    }
}
