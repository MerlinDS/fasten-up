using System;
using System.Collections.Generic;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Exceptions;

namespace FastenUp.Runtime.Base
{
    public sealed class Bindable<T> : IBindable<T>, IInternalBindable<T>
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

        void IInternalBindable<T>.Add(IBinder<T> binder)
        {
            if (_bindables.Contains(binder))
                throw new FastenUpException($"{nameof(binder)} already added to the {nameof(Bindable<T>)}.");

            binder.SetValue(_value);
            _bindables.Add(binder);

            if (binder is IGettableBinder<T> bindableGettable)
                bindableGettable.OnBinderChanged += OnValueChangedHandler;
        }

        void IInternalBindable<T>.Remove(IBinder<T> binder)
        {
            if (!_bindables.Contains(binder))
                throw new FastenUpException($"{nameof(binder)} not found in the {nameof(Bindable<T>)}.");

            _bindables.Remove(binder);
            
            if (binder is IGettableBinder<T> bindableGettable)
                bindableGettable.OnBinderChanged -= OnValueChangedHandler;
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