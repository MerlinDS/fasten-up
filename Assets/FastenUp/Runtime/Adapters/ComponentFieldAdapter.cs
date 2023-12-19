using System;
using JetBrains.Annotations;
using UnityEngine;

namespace FastenUp.Runtime.Adapters
{
    /// <summary>
    /// This is a base class for all component field <see cref="IComponentFieldAdapter{T}">adapters</see>.
    /// It provides a common constructor and a reference to the component.
    /// </summary>
    /// <typeparam name="TComponent">Type of the <see cref="Component"/> that will be adapted</typeparam>
    internal abstract class ComponentFieldAdapter<TComponent>
        where TComponent : Component
    {
        /// <summary>
        /// Creates an instance of the adapter if the <paramref name="gameObject"/> has the <typeparamref name="TComponent"/> component.
        /// </summary>
        /// <param name="gameObject"><see cref="GameObject"/> that contains the component</param>
        /// <typeparam name="TAdapter">Concrete type of the adapter</typeparam>
        /// <typeparam name="T">Type of the field that will be adapted</typeparam>
        /// <returns>Instance of the adapter or null if the <paramref name="gameObject"/> does not have the <typeparamref name="TComponent"/> component</returns>
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