using System;
using FastenUp.Runtime.Binders;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Bindable that provides the ability to bind values in the Unity components.
    /// </summary>
    /// <typeparam name="T">Type of the value</typeparam>
    public sealed class Bindable<T> : BaseBindable<T>
    {
        public Bindable(T value = default) : base(value)
        {
        }

        /// <inheritdoc />
        public override T Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                OnValueChanged?.Invoke(value);
            }
        }

        /// <summary>
        /// The event that will be invoked when the value is changed.
        /// </summary>
        public event Action<T> OnValueChanged;

        /// <inheritdoc />
        protected override void PostBind(IBinder<T> binder)
        {
            binder.OnBinderChanged += OnValueChangedHandler;
        }

        /// <inheritdoc />
        protected override void PostUnbind(IBinder<T> binder)
        {
            binder.OnBinderChanged -= OnValueChangedHandler;
        }
        
        private void OnValueChangedHandler(IBinder binder)
        {
            if (binder is not IValueProvider<T> valueProvider)
                return;

            var value = valueProvider.GetValue();
            UpdateBinders(value, valueProvider);
            SetValueSilently(value);
            OnValueChanged?.Invoke(value);
        }
    }
}