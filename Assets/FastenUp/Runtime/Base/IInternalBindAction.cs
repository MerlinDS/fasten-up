using FastenUp.Runtime.Bindables;

namespace FastenUp.Runtime.Base
{
    /// <summary>
    /// Used to bind <see cref="IBindableListener{T}"/>s to a <see cref="IMediator"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IInternalBindAction<out T> : IInternalBind
    {
        internal void AddListener(IBindableListener<T> listener);
        internal void RemoveListener(IBindableListener<T> listener);
    }
}