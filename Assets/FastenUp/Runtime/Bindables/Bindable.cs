using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Exceptions;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Bindable that provides the ability to bind values in the Unity components.
    /// </summary>
    /// <typeparam name="T">Type of the value</typeparam>
    public sealed class Bindable<T> : IBindable<T>
    {
        private readonly HashSet<IBinder<T>> _binders = new(1);

        private T _value;

        public Bindable(T value = default)
        {
            _value = value;
        }

        /// <summary>
        /// The value that will be bind to the Unity components.
        /// </summary>
        public T Value
        {
            get => _value;
            set
            {
                UpdateBinders(value);
                ChangeValueAndNotify(value);
            }
        }

        /// <summary>
        /// The event that will be invoked when the value is changed.
        /// </summary>
        public event Action<T> OnValueChanged;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IBindable<T>.Bind(IBinder<T> binder)
        {
            if (_binders.Contains(binder))
                throw new FastenUpException($"{nameof(binder)} already bind to the {nameof(Bindable<T>)}.");

            if(binder is IValueReceiver<T> valueReceiver)
                valueReceiver.SetValue(_value);
            
            _binders.Add(binder);
            binder.OnBinderChanged += OnValueChangedHandler;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IBindable<T>.Unbind(IBinder<T> binder)
        {
            if (!_binders.Contains(binder))
                throw new FastenUpException($"{nameof(binder)} not bind to the {nameof(Bindable<T>)}.");

            _binders.Remove(binder);
            
            binder.OnBinderChanged -= OnValueChangedHandler;
        }

        private void OnValueChangedHandler(IBinder binder)
        {
            if (binder is not IValueProvider<T> valueProvider)
                return;

            var value = valueProvider.GetValue();
            UpdateBinders(value, valueProvider);
            ChangeValueAndNotify(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateBinders(T value, IBinder<T> ignored = null)
        {
            foreach (var bindable in _binders)
            {
                if (bindable == ignored || bindable is not IValueReceiver<T> valueReceiver)
                    continue;

                valueReceiver.SetValue(value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ChangeValueAndNotify(T value)
        {
            _value = value;
            OnValueChanged?.Invoke(value);
        }
    }
}