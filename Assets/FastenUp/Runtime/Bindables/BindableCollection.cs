using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FastenUp.Runtime.Binders.Collections;

namespace FastenUp.Runtime.Bindables
{
    /// <inheritdoc />
    public sealed class BindableCollection<T> : IBindableCollection<T>
    // , ICollection<T> - we don't need this interface, because we want to avoid the boxing of the GetEnumerator method.
    {
        private readonly List<T> _items = new();
        private readonly BinderSet<ICollectionBinder<T>> _binders = new();

        /// <inheritdoc />
        public int Count => _items.Count;

        /// <inheritdoc />
        public void Add(T item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");

            _items.Add(item);
            foreach (var binder in _binders)
                binder.Add(item);
        }
        /// <inheritdoc />
        public bool Remove(T item)
        {
            if (item is null)
                return false;

            if (!_items.Remove(item))
                return false;

            foreach (var binder in _binders)
                binder.Remove(item);

            return true;
        }

        /// <inheritdoc />
        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        /// <inheritdoc />
        public void Clear()
        {
            if(_items.Count == 0)
                return;
            
            foreach (var binder in _binders)
                ClearBinder(binder);
            _items.Clear();
        }

        //This method is used by the foreach statement without boxing.
        /// <inheritdoc />
        public List<T>.Enumerator GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        /// <inheritdoc />
        void IBindableCollection<T>.Bind(ICollectionBinder<T> binder)
        {
            _binders.Add(binder);
            foreach (var item in _items)
                binder.Add(item);
        }

        /// <inheritdoc />
        void IBindableCollection<T>.Unbind(ICollectionBinder<T> binder)
        {
            _binders.Remove(binder);
            ClearBinder(binder);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ClearBinder(ICollectionBinder<T> binder)
        {
            foreach (var item in _items)
                binder.Remove(item);
        }
    }
}