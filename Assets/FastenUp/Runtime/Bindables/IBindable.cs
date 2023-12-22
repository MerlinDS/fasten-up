using System;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Mediators;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// This interface is used to bind <see cref="IBinder{T}"/>s to a <see cref="IMediator"/>.
    /// </summary>
    /// <typeparam name="T">Type of the value that will be set or get by the bindable component</typeparam>
    public interface IBindable<T>
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