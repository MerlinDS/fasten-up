using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Mediators;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Main interface for all bindables.
    /// </summary>
    /// <remarks>
    /// Used by source code generator to determine bindable properties.
    /// </remarks>
    public interface IBindable
    {
    }
    
    /// <summary>
    /// Used to bind <see cref="IBinder{T}"/>s to a <see cref="IMediator"/>.
    /// </summary>
    /// <typeparam name="T">Type of the value that will be set by the binder component</typeparam>
    public interface IBindable<T> : IBindable
    {
        internal void Bind(IBinder<T> binder);
        internal void Unbind(IBinder<T> binder);
    }
}