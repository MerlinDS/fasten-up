using FastenUp.Runtime.Base;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Implementations of this interface will be bound to an <see cref="IBindAction{T}"/>.
    /// It provides communication between the unity components and bind actions in a <see cref="IMediator"/>.
    /// </summary>
    public interface IBindableListener<in T> : IBindable
    {
        void AddListener(T action);
        void RemoveListener(T action);
    }
}