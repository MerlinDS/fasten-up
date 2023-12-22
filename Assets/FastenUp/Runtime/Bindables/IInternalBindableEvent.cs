using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Mediators;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Used to bind <see cref="IEventBinder{T}"/>s to a <see cref="IMediator"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IInternalBindableEvent<out T> : IInternalBindable
    {
        internal void Add(IEventBinder<T> eventBinder);
        internal void Remove(IEventBinder<T> eventBinder);
    }
}