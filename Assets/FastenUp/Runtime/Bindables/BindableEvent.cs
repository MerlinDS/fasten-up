using UnityEngine.Events;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Bindable event that provides the ability to subscribe to the events with one argument in the bind Unity components.
    /// </summary>
    /// <typeparam name="T">Type of the argument of the <see cref="UnityEvent{T}"/></typeparam>
    public sealed class BindableEvent<T> : BaseBindableEvent<UnityAction<T>>
    {
    }

    /// <summary>
    /// Bindable event that provides the ability to subscribe to the events without any arguments in the bind Unity components.
    /// </summary>
    public sealed class BindableEvent : BaseBindableEvent<UnityAction>
    {
    }
}