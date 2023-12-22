using FastenUp.Runtime.Base;
using FastenUp.Runtime.Mediators;

namespace FastenUp.Runtime.Binders
{
    /// <summary>
    /// Implementations of this interface will be bound to an <see cref="IBindAction{T}"/>.
    /// It provides communication between the unity components and bindable event in a <see cref="IMediator"/>.
    /// </summary>
    public interface IEventBinder<in T> : IBinder
    {
        void AddListener(T action);
        void RemoveListener(T action);
    }
}