using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedList
{
    public class MySortedListItem<T> where T : IComparable<T>
    {
        public T Value { get; set; }
        public MySortedListItem<T> Next { get; set; }
        public MySortedListItem<T> Prev { get; set; }
        public MySortedListItem(T item)
        {
            Value = item;
        }
    }
    public class MySortedList<T>:ICollection<T> where T : IComparable<T>
    {
        
        private MySortedListItem<T> _firstItem;
        private MySortedListItem<T> _currentItem;
        private MySortedListItem<T> _lastItem;
        public MySortedList() { }
        public MySortedList(IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            foreach (var item in source)
            {
                Add(item);
            }
        }
        public void Add(T item)
        {
            var newItem = new MySortedListItem<T>(item);
            _currentItem = _firstItem;
            if (Count == 0)
            {
                _lastItem = newItem;
                _firstItem = newItem;
            }
            else
            {
                _lastItem.Next = newItem;
                _lastItem = newItem;
            }
            Count++;
            Sort();
        }
        public void Sort()
        {
            int k = 0;
            T[] arr = new T[Count];
            for (var currentItem = _firstItem; currentItem != null; currentItem = currentItem.Next)
            {
                arr[k] = currentItem.Value;
                k++;
            }
            Array.Sort(arr);
            _currentItem = _firstItem;
            foreach(T el in arr)
            {
                _currentItem.Value = el;
                _currentItem = _currentItem.Next;
            }
        }
        public bool RemoveValue(T value)
        {
            _currentItem = _firstItem;
            MySortedListItem<T> previous = null;
            while (_currentItem != null)
            {
                if (_currentItem.Value.Equals(value))
                {
                    if (previous != null)
                    {
                        previous.Next = _currentItem.Next;
                        if (_currentItem.Next == null)
                            _lastItem = previous;
                    }
                    else
                    {
                        _firstItem = _firstItem.Next;
                        if (_firstItem == null)
                            _lastItem = null;
                    }
                    Count--;
                    return true;
                }
                previous = _currentItem;
                _currentItem = _currentItem.Next;
            }
            return false;
        }
        public bool Remove(T item)
        {
            if (!Contains(item))
            {
                return false;
            }
            RemoveValue(item);
            return true;
        }
        public void Clear()
        {
            _firstItem = null;
            _lastItem = null;
            Count = 0;
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (var currentItem = _firstItem; currentItem != null; currentItem = currentItem.Next)
            {
                yield return currentItem.Value;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public bool Contains(T item)
        {
            if (item == null)
            {
                return false;
            }
            foreach (var value in this)
            {
                if (item.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }
        public int IndexOf(T item)
        {
            int index = 0;
            if (item == null)
            {
                throw new InvalidOperationException("Item is empty");
            }
            foreach (var value in this)
            {
                if (item.Equals(value))
                {
                    return index;
                }
                index++;
            }
            return index;
        }
        public T GetByIndex(int index)
        {
            _currentItem = _firstItem;
            if (_firstItem == null)
            {
                throw new InvalidOperationException("SortList is empty");
            }
            if(index >= Count)
            {
                throw new IndexOutOfRangeException("Index out of bounce");
            }
            else
                for (int i = 0; i < index; i++)
                {
                    _currentItem = _currentItem.Next;
                }
            return _currentItem.Value;
        }
        public int Count { get; private set; }
        public bool IsReadOnly
        {
            get { return false; }
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            foreach (var item in this)
            {
                array[arrayIndex++] = item;
            }
        }
    }
}
