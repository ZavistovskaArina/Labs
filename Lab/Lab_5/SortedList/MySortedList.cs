using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedList
{
    /// <summary>
    ///  Delegate, using for evenets where you need parameters, in current situation - adding\deleting events.
    /// </summary>
    public delegate void EventDelegate<T>(object Sender, CollectionEventArgs<T> input);
    /// <summary>
    ///  Delegate where you need no parameters, in current situation - empty container.
    /// </summary>
    public delegate void EventClear<T>();
    //наследует класс EventArgs, который запихивают по шаблону
    /// <summary>
    ///  Class, using in delegate signature, stores the element which was added\deleted.
    /// </summary>
    
    public class CollectionEventArgs<T> : EventArgs
    {
        private T element;

        /// <summary>
        ///  Property to get or set current element.
        /// </summary>
        public T Element
        {
            get
            {
                return element;
            }
            set
            {
                element = value;
            }
        }

        /// <summary>
        ///  Constructor which takes element of current event.
        /// </summary>
        /// <param name="element">Input parametr</param>
        public CollectionEventArgs(T element)
        {
            this.Element = element;
        }
    }
    public class MySortedListItem<T> /*where T : IComparable<T>*/
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

        /// <summary>
        /// Default constructor
        /// </summary>
        public MySortedList() { }

        /// <summary>
        /// Constructor with parametr
        /// </summary>
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
        /// <summary>
        ///  Event, calls in case of add element
        /// </summary>
        public event EventDelegate<T> AddEvent = null;
        /// <summary>
        ///  Event, calls in case of delete element
        /// </summary>
        public event EventDelegate<T> RemoveEvent = null;
        /// <summary>
        ///  Event, calls in case of empty container
        /// </summary>
        public event EventClear<T> ClearEvent = null;

        /// <summary>
        ///  Using to add event
        /// </summary>
        protected void OnAddEvent(T input)
        {
            if (AddEvent != null)
            {
                CollectionEventArgs<T> current = new CollectionEventArgs<T>(input);
                AddEvent(this, current);
            }
        }
        /// <summary>
        ///  Using to delete event
        /// </summary>
        protected void OnRemoveEvent(T input)
        {
            if (RemoveEvent != null)
            {
                CollectionEventArgs<T> current = new CollectionEventArgs<T>(input);
                RemoveEvent(this, current);
            }
        }

        /// <summary>
        ///  Using to clear
        /// </summary>
        protected void onClearEvent()
        {
            if (ClearEvent != null)
            {
                ClearEvent();
            }
        }

        /// <summary>
        /// Add one value in the list
        /// </summary>
        /// <param name="item">Value to add</param>
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
            OnAddEvent(item);
            Sort();
        }

        /// <summary>
        /// Sort elements in list
        /// </summary>
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

        /// <summary>
        /// Remove element from list by value
        /// </summary>
        /// <param name="value">Value in the list</param>
        /// <returns>True if removed succesfully, false otherwise</returns>
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
                    OnRemoveEvent(value);
                    return true;
                }
                previous = _currentItem;
                _currentItem = _currentItem.Next;
            }
            return false;
        }
        /// <summary>
        /// Remove element from list by item
        /// </summary>
        /// <param name="item">Item in the list</param>
        /// <returns>True if removed succesfully, false otherwise</returns>
        public bool Remove(T item)
        {
            if (!Contains(item))
            {
                return false;
            }
            RemoveValue(item);
            return true;
        }

        /// <summary>
        /// Clear current list
        /// </summary>
        public void Clear()
        {
            _firstItem = null;
            _lastItem = null;
            Count = 0;
            onClearEvent();
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

        /// <summary>
        /// Check if list contains given item
        /// </summary>
        /// <param name="item">Item to find in the list</param>
        /// <returns>True if contains, false otherwise</returns>
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

        /// <summary>
        /// Return index of element from list by item
        /// </summary>
        /// <param name="item">Item to find in the list</param>
        /// <returns>Index if list contains it</returns>
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

        /// <summary>
        /// Returns value by index from the list
        /// </summary>
        /// <param name="index">Position in the list</param>
        /// <returns>Return value according to the given index</returns>
        public T GetByIndex(int index)
        {
            _currentItem = _firstItem;
            if (_firstItem == null)
            {
                throw new InvalidOperationException("SortList is empty");
            }
            if(index >= Count)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }
            else
                for (int i = 0; i < index; i++)
                {
                    _currentItem = _currentItem.Next;
                }
            return _currentItem.Value;
        }

        /// <summary>
        /// Returns number of values in list
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Flag for "read only" access
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Copy list to another list
        /// </summary>
        /// <param name="array">Where to copy</param>
        /// <param name="arrayIndex">Index to start from</param>
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
