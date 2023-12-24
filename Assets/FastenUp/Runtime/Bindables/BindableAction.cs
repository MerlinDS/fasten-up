using UnityEngine.Events;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Bindable action that provides the ability to invoke the <see cref="UnityEvent"/> in the bind Unity components.
    /// </summary>
    public sealed class BindableAction : BaseBindableAction<UnityEvent>
    {
        /// <inheritdoc cref="UnityEvent.Invoke"/>
        public void Invoke()
        {
            foreach (var binder in Binders)
                binder.OnAction.Invoke();
        }
    }

    /// <summary>
    /// Bindable action that provides the ability to invoke the <see cref="UnityEvent{T}"/> in the bind Unity components.
    /// </summary>
    public sealed class BindableAction<T> : BaseBindableAction<UnityEvent<T>>
    {
        /// <inheritdoc cref="UnityEvent{T}.Invoke(T)"/>
        public void Invoke(T arg)
        {
            foreach (var binder in Binders)
                binder.OnAction.Invoke(arg);
        }
    }
}