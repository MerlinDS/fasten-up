using System;
using System.Collections.Generic;
using FastenUp.Runtime.Binders.Events;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Base class for bindable events that provides the ability to subscribe to the event in the bind Unity components.
    /// </summary>
    /// <typeparam name="T">Type of the delegate to subscribe that is used in the bind Unity components.</typeparam>
    public abstract class BaseBindableEvent<T> : IBindableEvent<T>, IDisposable
    {
        private readonly BinderSet<IEventBinder<T>> _binders = new();

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
            foreach (IEventBinder<T> listener in _binders)
            {
                listener.AddListener(action);
            }
        }

        /// <summary>
        /// Unsubscribes the listener from the event.
        /// </summary>
        /// <param name="action">The action to unsubscribe from the event.</param>
        public void RemoveListener(T action)
        {
            _actions.Remove(action);
            foreach (IEventBinder<T> listener in _binders)
            {
                listener.RemoveListener(action);
            }
        }

        /// <inheritdoc />
        void IBindableEvent<T>.Bind(IEventBinder<T> eventBinder)
        {
            _binders.Add(eventBinder);
            foreach (T action in _actions)
            {
                eventBinder.AddListener(action);
            }
        }

        /// <inheritdoc />
        void IBindableEvent<T>.Unbind(IEventBinder<T> eventBinder)
        {
            _binders.Remove(eventBinder);
            foreach (T action in _actions)
            {
                eventBinder.RemoveListener(action);
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            foreach (IEventBinder<T> binder in _binders)
            {
                foreach (T action in _actions)
                {
                    binder.RemoveListener(action);
                }
            }

            _actions.Clear();
            _binders.Clear();
        }
    }
}