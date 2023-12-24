using FastenUp.Runtime.Binders.Actions;
using UnityEngine.Events;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Implementations of this interface can be bound to <see cref="IActionBinder{T}"/>s.
    /// It provides ability to invoke the <see cref="UnityEventBase"/> in the bind Unity components.
    /// </summary>
    /// /// <typeparam name="T">Type of the event, must be a subclass of <see cref="UnityEventBase"/></typeparam>
    public interface IBindableAction<in T> : IBindable
        where T : UnityEventBase, new()
    {
        internal void Bind(IActionBinder<T> actionBinder);
        internal void Unbind(IActionBinder<T> actionBinder);
    }
}