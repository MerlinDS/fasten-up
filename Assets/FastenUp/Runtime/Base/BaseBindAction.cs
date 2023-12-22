using System.Collections.Generic;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Exceptions;

namespace FastenUp.Runtime.Base
{
    public abstract class BaseBindAction<T> : IBindAction<T>
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
        
        protected void AddBindableListener(IEventBinder<T> listener)
        {
            if (_listeners.Contains(listener))
                throw new FastenUpException("Bindable already added to bind point.");

            foreach (var action in _actions)
                listener.AddListener(action);
            _listeners.Add(listener);
        }

        protected void RemoveBindableListener(IEventBinder<T> listener)
        {
            if (!_listeners.Contains(listener))
                throw new FastenUpException("Bindable not found in bind point.");

            foreach (var action in _actions)
                listener.RemoveListener(action);
            _listeners.Remove(listener);
        }
    }
}