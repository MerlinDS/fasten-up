using FastenUp.Runtime.Bindables;

namespace FastenUp.Runtime.Base
{
    /// <summary>
    /// Used to bind <see cref="IBindable{T}"/>s to a <see cref="IMediator"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IInternalBindPoint<out T> : IInternalBind
    {
        internal void Add(IBindable<T> bindable);
        internal void Remove(IBindable<T> bindable);
    }
}