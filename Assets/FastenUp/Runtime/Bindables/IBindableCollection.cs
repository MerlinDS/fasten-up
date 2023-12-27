using FastenUp.Runtime.Binders.Collections;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Represents a collection of objects that can be individually bound to a <see cref="ICollectionBinder{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    public interface IBindableCollection<out T> : IBindable
    {
        internal void Bind(ICollectionBinder<T> binder);
        internal void Unbind(ICollectionBinder<T> binder);
    }
}