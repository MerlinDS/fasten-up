using FastenUp.Runtime.Binders.Actions;
using UnityEngine.Events;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Bindable action that provides the ability to invoke the <see cref="UnityEvent"/> in the bind Unity components.
    /// </summary>
    public sealed class BindableAction : BaseBindableAction<UnityEvent>
    {
        private bool _wasInvoked;

        /// <inheritdoc cref="UnityEvent.Invoke"/>
        public void Invoke()
        {
            foreach (var binder in Binders)
                binder.OnAction.Invoke();
        }

        /// <inheritdoc />
        protected override void PostBind(IActionBinder<UnityEvent> actionBinder)
        {
            if (_wasInvoked)
                actionBinder.OnAction.Invoke();
        }
        
        protected override void PostUnBind()
        {
            _wasInvoked = false;
        }
    }

    /// <summary>
    /// Bindable action that provides the ability to invoke the <see cref="UnityEvent{T}"/> in the bind Unity components.
    /// </summary>
    public sealed class BindableAction<T> : BaseBindableAction<UnityEvent<T>>
    {
        private bool _wasInvoked;
        private T _previousArg;

        /// <inheritdoc cref="UnityEvent{T}.Invoke(T)"/>
        public void Invoke(T arg)
        {
            foreach (var binder in Binders)
                binder.OnAction.Invoke(arg);

            _previousArg = arg;
            _wasInvoked = true;
        }

        /// <inheritdoc />
        protected override void PostBind(IActionBinder<UnityEvent<T>> actionBinder)
        {
            if (_wasInvoked)
                actionBinder.OnAction.Invoke(_previousArg);
        }

        /// <inheritdoc />
        protected override void PostUnBind()
        {
            _wasInvoked = false;
            _previousArg = default;
        }
    }
}