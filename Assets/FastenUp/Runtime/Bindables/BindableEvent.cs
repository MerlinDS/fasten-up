using System.Collections.Generic;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Exceptions;
using UnityEngine.Events;

namespace FastenUp.Runtime.Bindables
{
    public sealed class BindableEvent<T> : BaseBindableEvent<UnityAction<T>>
    {
    }

    public sealed class BindableEvent : BaseBindableEvent<UnityAction>
    {
    }
    
    public abstract class BaseBindableEvent<T> : IBindableEvent<T>, IInternalBindableEvent<T>
    {
        private readonly HashSet<IEventBinder<T>> _listeners = new(1);

        private readonly List<T> _actions = new(1);

        /// <inheritdoc />
        public bool HasListeners(T action) =>
            _actions.Contains(action);

        /// <inheritdoc />
        public void AddListener(T action)
        {
            _actions.Add(action);
            foreach (var listener in _listeners)
                listener.AddListener(action);
        }

        /// <inheritdoc />
        public void RemoveListener(T action)
        {
            _actions.Remove(action);
            foreach (var listener in _listeners)
                listener.RemoveListener(action);
        }

        /// <inheritdoc />
        void IInternalBindableEvent<T>.Bind(IEventBinder<T> eventBinder)
        {
            if (_listeners.Contains(eventBinder))
                throw new FastenUpException($"{nameof(eventBinder)} already added to the {nameof(BindableEvent)}.");

            foreach (var action in _actions)
                eventBinder.AddListener(action);
            _listeners.Add(eventBinder);
        }

        /// <inheritdoc />
        void IInternalBindableEvent<T>.Unbind(IEventBinder<T> eventBinder)
        {
            if (!_listeners.Contains(eventBinder))
                throw new FastenUpException($"{nameof(eventBinder)} not found in the {nameof(BindableEvent)}.");

            foreach (var action in _actions)
                eventBinder.RemoveListener(action);
            _listeners.Remove(eventBinder);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            foreach (var listener in _listeners)
            {
                foreach (var action in _actions)
                    listener.RemoveListener(action);
            }

            _listeners.Clear();
            _actions.Clear();
        }
    }
}