using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace Sirloin
{

    public sealed class ObservableList : IObservableVector<object>, IReadOnlyList<object>, INotifyPropertyChanged
    {
        private readonly List<object> list;

        public event PropertyChangedEventHandler PropertyChanged;
        public event VectorChangedEventHandler<object> VectorChanged;

        public ObservableList()
        {
            this.list = new List<object>();
        }

        // WME1099
        /*
        public ObservableList(int capacity)
        {
            this.list = new List<object>(capacity);
        }
        */

        public ObservableList(IEnumerable<object> items)
        {
            this.list = new List<object>(items);
        }

        public int Count => list.Count;

        int IReadOnlyCollection<object>.Count => Count;

        public bool IsReadOnly => false;

        public object this[int index]
        {
            get { return list[index]; }
            set { list[index] = value; }
        }

        object IReadOnlyList<object>.this[int index] => this[index];

        public int IndexOf(object item) => list.IndexOf(item);

        public void Insert(int index, object item)
        {
            list.Insert(index, item);
            OnPropertyChanged(nameof(Count));
            // TODO: Binding for indexer? "Item" property.
            OnVectorChanged(CollectionChange.ItemInserted, (uint)index);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
            OnPropertyChanged(nameof(Count));
            // TODO: Binding for indexer? "Item" property.
            OnVectorChanged(CollectionChange.ItemRemoved, (uint)index);
        }

        public void Add(object item) =>
            Insert(this.Count, item);

        public void Clear()
        {
            list.Clear();
            OnPropertyChanged(nameof(Count));
            OnVectorChanged(CollectionChange.Reset, 0);
        }

        public bool Contains(object item) => list.Contains(item);

        public void CopyTo(object[] array, int arrayIndex) => 
            list.CopyTo(array, arrayIndex);

        public bool Remove(object item)
        {
            int index = this.IndexOf(item);
            if (index == -1)
                return false;

            this.RemoveAt(index);
            OnPropertyChanged(nameof(Count));
            OnVectorChanged(CollectionChange.ItemRemoved, (uint)index);
            return true;
        }

        public IEnumerator<object> GetEnumerator() => list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void OnVectorChanged(CollectionChange change, uint index)
        {
            VectorChanged?.Invoke(this, new VectorChangedArgs(change, index));
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    // TODO: Separate this out into a new library.
    internal sealed class ObservableVector<T> : Collection<T>, IObservableVector<T>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event VectorChangedEventHandler<T> VectorChanged;

        public ObservableVector()
            : base()
        { }

        public ObservableVector(IList<T> list)
            : base(list)
        { }

        protected override void ClearItems()
        {
            base.ClearItems();
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(nameof(Items));
            OnVectorChanged(CollectionChange.Reset, 0);
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(nameof(Items));
            OnVectorChanged(CollectionChange.ItemInserted, (uint)index);
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(nameof(Items));
            OnVectorChanged(CollectionChange.ItemRemoved, (uint)index);
        }

        protected override void SetItem(int index, T item)
        {
            base.SetItem(index, item);
            OnPropertyChanged(nameof(Items));
            OnVectorChanged(CollectionChange.ItemChanged, (uint)index);
        }

        private void OnVectorChanged(CollectionChange change, uint index)
        {
            VectorChanged?.Invoke(this, new VectorChangedArgs(change, index));
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
