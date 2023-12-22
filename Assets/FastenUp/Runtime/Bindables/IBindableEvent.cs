using System;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// The bindable event provides a way to bind actions to a <see cref="FastenUp.Runtime.Binders.IEventBinder{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of the action that will be invoked by the binder component</typeparam>
    public interface IBindableEvent<in T> : IDisposable
    {
        /// <summary>
        /// Checks if event has listeners.
        /// </summary>
        /// <param name="action"> Listener to check. </param>
        /// <returns> True if event has listeners. </returns>
        bool HasListeners(T action);

        /// <summary>
        /// Adds listener to all added binders.
        /// </summary>
        /// <param name="action"> Action to add. </param>
        void AddListener(T action);

        /// <summary>
        /// Removes listener from all added binders.
        /// </summary>
        /// <param name="action"> Action to remove. </param>
        void RemoveListener(T action);
    }
}