using FastenUp.Runtime.Binders.Actions;
using UnityEngine.Events;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Base class for bindable actions that provides the ability to invoke the <see cref="UnityEventBase"/> in the bind Unity components.
    /// </summary>
    /// <typeparam name="T">Type of the argument of the <see cref="UnityEventBase"/></typeparam>
    public abstract class BaseBindableAction<T> : IBindableAction<T> where T : UnityEventBase, new()
    {
        /// <summary>
        /// All binders that are bound to this bindable action.
        /// </summary>
        internal BinderSet<IActionBinder<T>> Binders { get; } = new();

        /// <inheritdoc />
        void IBindableAction<T>.Bind(IActionBinder<T> actionBinder)
        {
            
            if(actionBinder.OnAction == null)
                return;

            Binders.Add(actionBinder);
            PostBind(actionBinder);
        }
        
        /// <inheritdoc />
        void IBindableAction<T>.Unbind(IActionBinder<T> actionBinder)
        {
            Binders.Remove(actionBinder);
        }

        protected abstract void PostBind(IActionBinder<T> actionBinder);
        protected abstract void PostUnBind();
    }
}