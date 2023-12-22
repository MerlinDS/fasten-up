using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Mediators;

namespace FastenUp.Runtime.Base
{
    /// <summary>
    /// Main interface for all bindable properties.
    /// Used by source code generator to determine bindable properties.
    /// </summary>
    public interface IInternalBindable
    {
    }
    
    /// <summary>
    /// Used to bind <see cref="IBinder{T}"/>s to a <see cref="IMediator"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IInternalBindable<out T> : IInternalBindable
    {
        internal void Add(IBinder<T> binder);
        internal void Remove(IBinder<T> binder);
    }
}