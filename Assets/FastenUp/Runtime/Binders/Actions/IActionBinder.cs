using UnityEngine.Events;

namespace FastenUp.Runtime.Binders.Actions
{
    /// <summary>
    /// Implementation of this interface will be bound to an <see cref="FastenUp.Runtime.Bindables.IBindableAction{T}"/>.
    /// It provides the ability to invoke the <see cref="UnityEventBase"/> in the bind Unity components.
    /// </summary>
    /// <typeparam name="T">Type of the event, must be a subclass of <see cref="UnityEventBase"/></typeparam>
    public interface IActionBinder<out T> : IBinder
        where T : UnityEventBase, new()
    {
        /// <summary>
        /// The <see cref="UnityEventBase"/> that will be invoked in the bind Unity components.
        /// </summary>
        T OnAction { get; }
    }
}