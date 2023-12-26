using JetBrains.Annotations;

namespace FastenUp.Runtime.Binders.Collections
{
    /// <summary>
    /// <see cref="IBinder"/> that provides the ability to bind the collection of items.
    /// </summary>
    /// <typeparam name="T">Type of the item.</typeparam>
    public interface ICollectionBinder<in T> : IBinder
    {
        /// <summary>
        /// Adds item to the bound collection.
        /// </summary>
        /// <param name="item">Item to add.</param>
        void Add([NotNull] T item);
        
        /// <summary>
        /// Removes item from the bound collection.
        /// </summary>
        /// <param name="item"></param>
        void Remove([NotNull] T item);
    }
}