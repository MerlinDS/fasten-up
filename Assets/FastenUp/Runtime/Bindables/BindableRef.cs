using System;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Bindable reference is used to bind the reference of the specific type to the mediator.
    /// It can be used, for example, to bind the reference of one mediator to another mediator.
    /// </summary>
    /// <typeparam name="T">The type of the referenced object.</typeparam>
    public sealed class BindableRef<T> : IBindableRef<T>
        where T : class
    {
        /// <summary>
        /// This event will be invoked when the referenced object is changed.
        /// </summary>
        public event Action OnRefChanged;

        /// <summary>
        /// The referenced object.
        /// </summary>
        public T Value { get; private set; }
        
        /// <inheritdoc />
        void IBindableRef<T>.Bind(T reference)
        {
            Value = reference;
            OnRefChanged?.Invoke();
        }

        /// <inheritdoc />
        void IBindableRef<T>.Unbind(T reference)
        {
            Value = null;
            OnRefChanged?.Invoke();
        }
    }
}