using System;
using FastenUp.Runtime.Binders;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Bindable reference is used to bind the reference of the specific type to the mediator.
    /// It can be used, for example, to bind the reference of one mediator to another mediator.
    /// </summary>
    /// <typeparam name="T">The type of the referenced object.</typeparam>
    public sealed class BindableRef<T> : IBindable<T>
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
        void IBindable<T>.Bind(IBinder<T> binder)
        {
            if (binder is not IValueProvider<T> valueReceiver)
                return;

            Value = valueReceiver.GetValue();
            OnRefChanged?.Invoke();
        }

        /// <inheritdoc />
        void IBindable<T>.Unbind(IBinder<T> binder)
        {
            if (binder is not IValueProvider<T> valueReceiver)
                return;

            if (Value is null || Value != valueReceiver.GetValue())
                return;

            Value = null;
            OnRefChanged?.Invoke();
        }
    }
}