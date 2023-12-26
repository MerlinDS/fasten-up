using System.Collections.Generic;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Exceptions;
using JetBrains.Annotations;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Internal collection of binders.
    /// Used to prevent duplicate binders.
    /// </summary>
    /// <typeparam name="T">Type of the binder.</typeparam>
    internal sealed class BinderSet<T>
        where T : IBinder
    {
        private readonly HashSet<T> _binder;

        public BinderSet(int capacity = 1)
        {
            _binder = new HashSet<T>(capacity);
        }

        /// <summary>
        /// Adds binder to the collection.
        /// </summary>
        /// <param name="binder">Binder to add.</param>
        /// <exception cref="FastenUpException">If binder already bind to the <see cref="IBindable"/>.</exception>
        public void Add([NotNull] T binder)
        {
            if(!_binder.Add(binder))
                throw new FastenUpException($"{binder} already bind to the {nameof(IBindable)}.");
        }
        
        /// <summary>
        /// Removes binder from the collection.
        /// </summary>
        /// <param name="binder">Binder to remove.</param>
        /// <exception cref="FastenUpException">If binder not bind to the <see cref="IBindable"/>.</exception>
        public void Remove([NotNull] T binder)
        {
            if(!_binder.Remove(binder))
                throw new FastenUpException($"{binder} not bind to the {nameof(IBindable)}.");
        }

        /// <summary>
        /// Gets the enumerator of the collection.
        /// </summary>
        /// <remarks>
        /// We need to return the enumerator of the <see cref="HashSet{T}"/> to prevent boxing.
        /// </remarks>
        public HashSet<T>.Enumerator GetEnumerator()
        {
            return _binder.GetEnumerator();
        }
    }
}