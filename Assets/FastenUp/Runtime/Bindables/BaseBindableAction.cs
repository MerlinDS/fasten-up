using System.Collections.Generic;
using FastenUp.Runtime.Binders.Actions;
using FastenUp.Runtime.Exceptions;
using UnityEngine.Events;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Base class for bindable actions that provides the ability to invoke the <see cref="UnityEventBase"/> in the bind Unity components.
    /// </summary>
    /// <typeparam name="T">Type of the argument of the <see cref="UnityEventBase"/></typeparam>
    public abstract class BaseBindableAction<T> : IBindableAction<T> where T : UnityEventBase, new()
    {
        private readonly HashSet<IActionBinder<T>> _binders = new(1);

        /// <summary>
        /// All binders that are bound to this bindable action.
        /// </summary>
        protected IEnumerable<IActionBinder<T>> Binders => _binders;

        /// <inheritdoc />
        void IBindableAction<T>.Bind(IActionBinder<T> actionBinder)
        {
            if(_binders.Contains(actionBinder))
                throw new FastenUpException($"{nameof(actionBinder)} already bind to {this}.");
            
            if(actionBinder.OnAction == null)
                return;

            _binders.Add(actionBinder);
        }
        
        /// <inheritdoc />
        void IBindableAction<T>.Unbind(IActionBinder<T> actionBinder)
        {
            if(!_binders.Contains(actionBinder))
                throw new FastenUpException($"{nameof(actionBinder)} not bind to {this}.");
            
            _binders.Remove(actionBinder);
        }
    }
}