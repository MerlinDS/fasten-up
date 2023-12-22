using System;

namespace FastenUp.Runtime.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBindableEvent<in T> : IDisposable
    {
        /// <summary>
        /// Checks if bind action has listeners.
        /// </summary>
        /// <param name="action"> Action to check. </param>
        /// <returns> True if bind action has listeners. </returns>
        bool HasListeners(T action);

        /// <summary>
        /// Adds listener to all bindables.
        /// </summary>
        /// <param name="action"> Action to add. </param>
        void AddListener(T action);

        /// <summary>
        /// Removes listener from all bindables.
        /// </summary>
        /// <param name="action"> Action to remove. </param>
        void RemoveListener(T action);
    }
}