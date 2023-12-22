using System;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Mediators;

namespace FastenUp.Runtime.Base
{
    /// <summary>
    /// This interface is used to bind <see cref="IBindable{T}"/>s to a <see cref="IMediator"/>.
    /// </summary>
    /// <typeparam name="T">Type of the value that will be set or get by the bindable component</typeparam>
    public interface IBindPoint<T>
    {
        /// <summary>
        /// The value that will be set or get by the bindable component.
        /// </summary>
        T Value { get; set; }
        /// <summary>
        /// Event that will be invoked when the value of the bind point changes.
        /// </summary>
        event Action<T> OnValueChanged;
    }
}