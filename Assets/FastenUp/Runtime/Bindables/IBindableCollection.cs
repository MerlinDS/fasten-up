using System.Collections.Generic;
using FastenUp.Runtime.Binders.Collections;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Represents a collection of objects that can be individually bound to a <see cref="ICollectionBinder{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    public interface IBindableCollection<T> : IBindable
    {
        internal void Bind(ICollectionBinder<T> binder);
        internal void Unbind(ICollectionBinder<T> binder);

        /// <summary>
        /// Gets the number of elements contained in the <see cref="IBindableCollection{T}"/>.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Adds an item to the <see cref="IBindableCollection{T}"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="IBindableCollection{T}"/>.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="item"/> is null.</exception>
        void Add(T item);

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="IBindableCollection{T}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="IBindableCollection{T}"/>.</param>
        /// <returns>true if <paramref name="item"/> was successfully removed from the <see cref="IBindableCollection{T}"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="IBindableCollection{T}"/>.</returns>
        bool Remove(T item);

        /// <summary>
        /// Determines whether the <see cref="IBindableCollection{T}"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="IBindableCollection{T}"/>.</param>
        /// <returns>true if <paramref name="item"/> is found in the <see cref="IBindableCollection{T}"/>; otherwise, false.</returns>
        bool Contains(T item);

        /// <summary>
        /// Removes all items from the <see cref="IBindableCollection{T}"/>.
        /// </summary>
        void Clear();

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="IBindableCollection{T}"/>.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the <see cref="IBindableCollection{T}"/>.</returns>
        List<T>.Enumerator GetEnumerator();
    }
}