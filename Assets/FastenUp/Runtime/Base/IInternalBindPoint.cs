using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Mediators;

namespace FastenUp.Runtime.Base
{
    /// <summary>
    /// Used to bind <see cref="IBinder{T}"/>s to a <see cref="IMediator"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IInternalBindPoint<out T> : IInternalBind
    {
        internal void Add(IBinder<T> binder);
        internal void Remove(IBinder<T> binder);
    }
}