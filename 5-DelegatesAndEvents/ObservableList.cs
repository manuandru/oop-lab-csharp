namespace DelegatesAndEvents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <inheritdoc cref="IObservableList{T}" />
    public class ObservableList<TItem> : IObservableList<TItem>
    {
        private readonly IList<TItem> list = new List<TItem>();

        /// <inheritdoc cref="IObservableList{T}.ElementInserted" />
        public event ListChangeCallback<TItem> ElementInserted;

        /// <inheritdoc cref="IObservableList{T}.ElementRemoved" />
        public event ListChangeCallback<TItem> ElementRemoved;

        /// <inheritdoc cref="IObservableList{T}.ElementChanged" />
        public event ListElementChangeCallback<TItem> ElementChanged;

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count
        {
            get
            {
                return this.list.Count();
            }
        }

        /// <inheritdoc cref="ICollection{T}.IsReadOnly" />
        public bool IsReadOnly
        {
            get
            {
                return this.list.IsReadOnly;
            }
        }

        /// <inheritdoc cref="IList{T}.this" />
        public TItem this[int index]
        {
            get { return this.list[index]; }
            set
            {
                TItem oldValue = list[index];
                list[index] = value;
                this.ElementChanged?.Invoke(this, value, oldValue, index);
            }
        }

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
        public IEnumerator<TItem> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <inheritdoc cref="ICollection{T}.Add" />
        public void Add(TItem item)
        {
            this.list.Add(item);
            this.ElementInserted?.Invoke(this, item, this.list.Count - 1);
        }

        /// <inheritdoc cref="ICollection{T}.Clear" />
        public void Clear()
        {
            foreach (TItem item in this.list)
            {
                this.Remove(item);
            }
        }

        /// <inheritdoc cref="ICollection{T}.Contains" />
        public bool Contains(TItem item)
        {
            return this.list.Contains(item);
        }

        /// <inheritdoc cref="ICollection{T}.CopyTo" />
        public void CopyTo(TItem[] array, int arrayIndex)
        {
            for (int i = arrayIndex; i < array.Length; i++)
            {
                array[i] = this[i - arrayIndex];
            }
        }

        /// <inheritdoc cref="ICollection{T}.Remove" />
        public bool Remove(TItem item)
        {
            int removedIndex = this.list.IndexOf(item);
            if (removedIndex != -1)
            {
                this.ElementRemoved?.Invoke(this, item, removedIndex);
            }
            return this.list.Remove(item);
        }

        /// <inheritdoc cref="IList{T}.IndexOf" />
        public int IndexOf(TItem item)
        {
            return this.IndexOf(item);
        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void Insert(int index, TItem item)
        {
            this.list.Insert(index, item);
            this.ElementInserted?.Invoke(this, item, index);
        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void RemoveAt(int index)
        {
            if(index >= 0 && index < this.Count)
            {
                TItem removedElem = this.list[index];
                this.list.RemoveAt(index);
                this.ElementRemoved?.Invoke(this, removedElem, index);
            }
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj)
        {
            return obj is ObservableList<TItem> list2 ? this.list.SequenceEqual(list2) : false;
        }

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode()
        {
            return HashCode.Combine(this.list);
        }

        /// <inheritdoc cref="object.ToString" />
        public override string ToString()
        {
            return this.list.ToString();
        }
    }
}
