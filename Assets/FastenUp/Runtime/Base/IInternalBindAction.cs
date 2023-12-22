using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Mediators;

namespace FastenUp.Runtime.Base
{
    /// <summary>
    /// Used to bind <see cref="IEventBinder{T}"/>s to a <see cref="IMediator"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IInternalBindAction<out T> : IInternalBind
    {
        internal void AddListener(IEventBinder<T> listener);
        internal void RemoveListener(IEventBinder<T> listener);
    }
}