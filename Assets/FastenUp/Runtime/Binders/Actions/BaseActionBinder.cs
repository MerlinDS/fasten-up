using UnityEngine;
using UnityEngine.Events;

namespace FastenUp.Runtime.Binders.Actions
{
    /// <summary>
    /// Base class for all action binders.
    /// </summary>
    /// <inheritdoc cref="IActionBinder{T}" path="/typeparam[@name='T']"/>
    public abstract class BaseActionBinder<T> : BaseBinder, IActionBinder<T>
        where T : UnityEventBase, new()
    {
        /// <inheritdoc />
        [field: SerializeField] public T OnAction { get; private set; } = new();
    }
}