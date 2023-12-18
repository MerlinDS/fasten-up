using System;
using JetBrains.Annotations;
using UnityEngine;

namespace FastenUp.Runtime.Adapters
{
    public abstract class ComponentFieldAdapter<TComponent>
        where TComponent : Component
    {
        [CanBeNull]
        protected static IComponentFieldAdapter<T> Create<TAdapter, T>(GameObject gameObject)
        {
            if (!gameObject.TryGetComponent(out TComponent component))
                return null;

            var adapter = Activator.CreateInstance(typeof(TAdapter), new object[] { component });
            return adapter as IComponentFieldAdapter<T>;
        }

        protected ComponentFieldAdapter(TComponent component)
        {
            Component = component;
        }

        protected TComponent Component { get; }
    }
}