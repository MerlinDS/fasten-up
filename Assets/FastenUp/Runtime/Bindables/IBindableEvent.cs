using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Mediators;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Used to bind <see cref="IEventBinder{T}"/>s to a <see cref="IMediator"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBindableEvent<out T> : IBindable
    {
        internal void Bind(IEventBinder<T> eventBinder);
        internal void Unbind(IEventBinder<T> eventBinder);
    }
}