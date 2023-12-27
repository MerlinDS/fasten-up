using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FastenUp.Runtime.Binders.Collections;

namespace FastenUp.Runtime.Bindables
{
    /// <inheritdoc cref="FastenUp.Runtime.Bindables.IBindableCollection{T}" />
    public sealed class BindableCollection<T> : IBindableCollection<T>, ICollection<T>
    {
        private readonly List<T> _items = new();
        private readonly BinderSet<ICollectionBinder<T>> _binders = new();

        /// <inheritdoc />
        public int Count => _items.Count;

        public event Action<T> OnItemAdded;
        public event Action<T> OnItemRemoved;

        /// <inheritdoc />
        public void Add(T item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");

            _items.Add(item);
            foreach (var binder in _binders)
                binder.Add(item);
            
            OnItemAdded?.Invoke(item);
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

            OnItemRemoved?.Invoke(item);
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

            if (OnItemRemoved is not null)
            {
                foreach (var item in _items) 
                    OnItemRemoved.Invoke(item);
            }
            
            _items.Clear();
        }

        /// <inheritdoc />
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
        
        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _items).GetEnumerator();
        }

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        bool ICollection<T>.IsReadOnly => false;

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