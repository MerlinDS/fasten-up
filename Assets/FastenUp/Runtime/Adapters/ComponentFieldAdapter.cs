using System;
using UnityEngine;

namespace FastenUp.Runtime.Adapters
{
    public abstract class ComponentFieldAdapter<TComponent>
        where TComponent : Component
    {
        protected static bool TryCreate<TAdapter, T>(GameObject gameObject,
            out IComponentFieldAdapter<T> adapter)
        {
            adapter = null;
            if (!gameObject.TryGetComponent(out TComponent component))
                return false;

            var instance = Activator.CreateInstance(typeof(TAdapter), new object[] { component });
            if (instance is not IComponentFieldAdapter<T> adapterT)
                return false;

            adapter = adapterT;
            return true;
        }

        protected ComponentFieldAdapter(TComponent component)
        {
            Component = component;
        }

        protected TComponent Component { get; }
    }
}