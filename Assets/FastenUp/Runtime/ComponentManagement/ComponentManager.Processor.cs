using System;
using System.Collections.Generic;

namespace FastenUp.Runtime.ComponentManagement
{
    public partial class ComponentManager
    {
        public sealed class Processor<TValue> : IProcessor
        {
            private readonly ComponentManager _manager;
            private readonly List<Action<TValue>> _setters = new();
            private readonly List<Func<TValue>> _getters = new();

            internal Processor(ComponentManager manager)
            {
                _manager = manager;
            }
            
            public Processor<TValue> AddSetter<TComponent>(Action<TComponent, TValue> setter)
            {
                if (!_manager.TryGetComponent<TComponent>(out var component))
                    return this;

                _setters.Add(value => setter.Invoke(component, value));
                return this;
            }
            
            public Processor<TValue> AddGetter<TComponent>(Func<TComponent, TValue> getter)
            {
                if (!_manager.TryGetComponent<TComponent>(out var component))
                    return this;

                _getters.Add(() => getter.Invoke(component));
                return this;
            }

            internal void SetValue(TValue value)
            {
                foreach (var setter in _setters)
                    setter.Invoke(value);
            }
            
            internal TValue GetValue()
            {
                foreach (var getter in _getters)
                    return getter.Invoke();

                return default;
            }
        }

        private interface IProcessor
        {
        }
    }
}