using ScheduleTest.Infrastructure.Extensions.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


namespace ScheduleTest.Infrastructure.Extensions
{
    public class VirtualizingCollection<T> : IList<T>, IList
    {
        #region Constructors

        public VirtualizingCollection(IItemsProvider<T> itemsProvider, int pageSize, int pageTimeout)
        {
            _itemsProvider = itemsProvider;
            _pageSize = pageSize;
            _pageTimeout = pageTimeout;
        }
        public VirtualizingCollection(IItemsProvider<T> itemsProvider, int pageSize)
        {
            _itemsProvider = itemsProvider;
            _pageSize = pageSize;
        }

        public VirtualizingCollection(IItemsProvider<T> itemsProvider)
        {
            _itemsProvider = itemsProvider;
        }

        #endregion

        #region ItemsProvider

        private readonly IItemsProvider<T> _itemsProvider;

        public IItemsProvider<T> ItemsProvider
        {
            get { return _itemsProvider; }
        }

        #endregion

        #region PageSize

        private readonly int _pageSize = 100;

        public int PageSize
        {
            get { return _pageSize; }
        }

        #endregion

        #region PageTimeout

        private readonly long _pageTimeout = 10000;

        public long PageTimeout
        {
            get { return _pageTimeout; }
        }

        #endregion

        #region IList<T>, IList

        #region Count

        private int _count = -1;

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// The first time this property is accessed, it will fetch the count from the IItemsProvider.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public virtual int Count
        {
            get
            {
                if (_count == -1)
                {
                    LoadCount();
                }
                return _count;
            }
            protected set
            {
                _count = value;
            }
        }

        #endregion

        #region Indexer

        public T this[int index]
        {
            get
            {
                int pageIndex = index / PageSize;
                int pageOffset = index % PageSize;

                RequestPage(pageIndex);

                if (pageOffset > PageSize / 2 && pageIndex < Count / PageSize)
                    RequestPage(pageIndex + 1);

                if (pageOffset < PageSize / 2 && pageIndex > 0)
                    RequestPage(pageIndex - 1);

                CleanUpPages();

                if (_pages[pageIndex] == null)
                    return default;


                return _pages[pageIndex][pageOffset];
            }
            set { throw new NotSupportedException(); }
        }

        object IList.this[int index]
        {
            get { return this[index]; }
            set { throw new NotSupportedException(); }
        }

        #endregion

        #region IEnumerator<T>, IEnumerator

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Add

        public void Add(T item)
        {
            ItemsProvider.AddItem(item);
        }

        int IList.Add(object value)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region Contains

        bool IList.Contains(object value)
        {
            return Contains((T)value);
        }

      
        public bool Contains(T item)
        {
            return false;
        }

        #endregion

        #region Clear


        public void Clear()
        {
            throw new NotSupportedException();
        }

        #endregion

        #region IndexOf

        int IList.IndexOf(object value)
        {
            return IndexOf((T)value);
        }

        public int IndexOf(T item)
        {
            return -1;
        }

        #endregion

        #region Insert


        public void Insert(int index, T item)
        {
            throw new NotSupportedException();
        }

        void IList.Insert(int index, object value)
        {
            Insert(index, (T)value);
        }

        #endregion

        #region Remove


        public void RemoveAt(int index)
        {
            ItemsProvider.RemoveItem(index);
        }

        void IList.Remove(object value)
        {
            throw new NotSupportedException();
        }

        public bool Remove(T item)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region CopyTo

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        void ICollection.CopyTo(Array array, int index)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region Misc

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </returns>
        public object SyncRoot
        {
            get { return this; }
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe).
        /// </summary>
        /// <value></value>
        /// <returns>Always false.
        /// </returns>
        public bool IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <value></value>
        /// <returns>Always true.
        /// </returns>
        public bool IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.IList"/> has a fixed size.
        /// </summary>
        /// <value></value>
        /// <returns>Always false.
        /// </returns>
        public bool IsFixedSize
        {
            get { return false; }
        }

        #endregion

        #endregion

        #region Paging

        protected readonly Dictionary<int, IList<T>> _pages = new Dictionary<int, IList<T>>();
        private readonly Dictionary<int, DateTime> _pageTouchTimes = new Dictionary<int, DateTime>();

        
        public void CleanUpPages()
        {
            List<int> keys = new List<int>(_pageTouchTimes.Keys);
            foreach (int key in keys)
            {
                // page 0 is a special case, since WPF ItemsControl access the first item frequently
                if (key != 0 && (DateTime.Now - _pageTouchTimes[key]).TotalMilliseconds > PageTimeout)
                {
                    _pages.Remove(key);
                    _pageTouchTimes.Remove(key);
                    Trace.WriteLine("Removed Page: " + key);
                }
            }
        }

        protected virtual void PopulatePage(int pageIndex, IList<T> page)
        {
            Trace.WriteLine("Page populated: " + pageIndex);
            if (_pages.ContainsKey(pageIndex))
                _pages[pageIndex] = page;
        }


        protected virtual void RequestPage(int pageIndex)
        {
            if (!_pages.ContainsKey(pageIndex))
            {
                _pages.Add(pageIndex, null);
                _pageTouchTimes.Add(pageIndex, DateTime.Now);
                Trace.WriteLine("Added page: " + pageIndex);
                LoadPage(pageIndex);
            }
            else
            {
                _pageTouchTimes[pageIndex] = DateTime.Now;
            }
        }

        #endregion

        #region Load methods

        protected virtual void LoadCount()
        {
            Count = FetchCount();
        }

        protected virtual void LoadPage(int pageIndex)
        {
            PopulatePage(pageIndex, FetchPage(pageIndex));
        }

        #endregion

        #region Fetch methods


        protected IList<T> FetchPage(int pageIndex)
        {
            return ItemsProvider.FetchRange(pageIndex * PageSize, PageSize);
        }


        protected int FetchCount()
        {
            return ItemsProvider.FetchCount();
        }

        #endregion
    }
}
