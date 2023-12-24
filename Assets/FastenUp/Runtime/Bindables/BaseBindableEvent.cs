using System;
using System.Collections.Generic;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Exceptions;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Base class for bindable events that provides the ability to subscribe to the event in the bind Unity components.
    /// </summary>
    /// <typeparam name="T">Type of the delegate to subscribe that is used in the bind Unity components.</typeparam>
    public abstract class BaseBindableEvent<T> : IBindableEvent<T>, IDisposable
    {
        private readonly HashSet<IEventBinder<T>> _listeners = new(1);

        private readonly List<T> _actions = new(1);

        /// <summary>
        /// Checks if the event has any listeners.
        /// </summary>
        /// <param name="action"> The action to check for listeners.</param>
        /// <returns>True if the listener was subscribed to the event. False otherwise.</returns>
        public bool HasListeners(T action) =>
            _actions.Contains(action);

        /// <summary>
        /// Subscribes the listener to the event.
        /// </summary>
        /// <param name="action">The action to subscribe to the event.</param>
        public void AddListener(T action)
        {
            _actions.Add(action);
            foreach (var listener in _listeners)
                listener.AddListener(action);
        }

        /// <summary>
        /// Unsubscribes the listener from the event.
        /// </summary>
        /// <param name="action">The action to unsubscribe from the event.</param>
        public void RemoveListener(T action)
        {
            _actions.Remove(action);
            foreach (var listener in _listeners)
                listener.RemoveListener(action);
        }

        /// <inheritdoc />
        void IBindableEvent<T>.Bind(IEventBinder<T> eventBinder)
        {
            if (_listeners.Contains(eventBinder))
                throw new FastenUpException($"{nameof(eventBinder)} already added to the {nameof(BindableEvent)}.");

            foreach (var action in _actions)
                eventBinder.AddListener(action);
            _listeners.Add(eventBinder);
        }

        /// <inheritdoc />
        void IBindableEvent<T>.Unbind(IEventBinder<T> eventBinder)
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