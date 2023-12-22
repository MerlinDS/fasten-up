using System;
using System.Collections.Generic;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Exceptions;

namespace FastenUp.Runtime.Base
{
    public sealed class BindPoint<T> : IBindPoint<T>, IInternalBindPoint<T>
    {
        private readonly HashSet<IBinder<T>> _bindables = new(1);

        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                foreach (var bindable in _bindables)
                    bindable.SetValue(value);
                
                ChangeValue(value);
            }
        }

        public event Action<T> OnValueChanged;

        void IInternalBindPoint<T>.Add(IBinder<T> binder)
        {
            if (_bindables.Contains(binder))
                throw new FastenUpException("Bindable already added to bind point.");

            binder.SetValue(_value);
            _bindables.Add(binder);

            if (binder is IGettableBinder<T> bindableGettable)
                bindableGettable.OnBindableChanged += OnValueChangedHandler;
        }

        void IInternalBindPoint<T>.Remove(IBinder<T> binder)
        {
            if (!_bindables.Contains(binder))
                throw new FastenUpException("Bindable not found in bind point.");

            _bindables.Remove(binder);
            
            if (binder is IGettableBinder<T> bindableGettable)
                bindableGettable.OnBindableChanged -= OnValueChangedHandler;
        }

        private void OnValueChangedHandler(IBinder binder)
        {
            if (binder is IGettableBinder<T> bindableT)
                ChangeValue(bindableT.GetValue());
        }

        private void ChangeValue(T value)
        {
            _value = value;
            OnValueChanged?.Invoke(value);
        }
    }
}