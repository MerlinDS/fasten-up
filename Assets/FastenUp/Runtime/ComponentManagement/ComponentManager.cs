using System;
using System.Collections.Generic;
using UnityEngine;

namespace FastenUp.Runtime.ComponentManagement
{
    public partial class ComponentManager : IComponentManager, IComponentProcessorRegistrar
    {
        private readonly GameObject _source;
        private readonly List<IProcessor> _processors = new();

        public ComponentManager(GameObject source)
        {
            _source = source;
        }

        public void Register<TValue>(Func<Processor<TValue>, Processor<TValue>> setup) => 
            _processors.Add(setup.Invoke(new Processor<TValue>(this)));

        public void UnregisterAll() => 
            _processors.Clear();

        /// <inheritdoc />
        public void SetValue<T>(T value)
        {
            foreach (var processor in _processors)
            {
                if (processor is Processor<T> concreteProcessor)
                    concreteProcessor.SetValue(value);
            }
        }
        
        /// <inheritdoc />
        public T GetValue<T>()
        {
            foreach (var processor in _processors)
            {
                if (processor is Processor<T> concreteProcessor)
                    return concreteProcessor.GetValue();
            }

            return default;
        }

        private bool TryGetComponent<T>(out T component) =>
            _source.TryGetComponent(out component);
    }
}