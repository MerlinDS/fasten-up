using System;
using System.Collections.Generic;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Exceptions;

namespace FastenUp.Runtime.Base
{
    public sealed class BindPoint<T> : IBindPoint<T>, IInternalBindPoint<T>
    {
        private readonly HashSet<IBindable<T>> _bindables = new(1);

        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                foreach (var bindable in _bindables)
                    bindable.SetValue(value);
            }
        }

        public event Action<T> OnValueChanged;

        void IInternalBindPoint<T>.Add(IBindable<T> bindable)
        {
            if (_bindables.Contains(bindable))
                throw new FastenUpException("Bindable already added to bind point.");

            bindable.SetValue(_value);
            _bindables.Add(bindable);

            if (bindable is IGettableBindable<T> bindableGettable)
                bindableGettable.OnBindableChanged += OnValueChangedHandler;
        }

        void IInternalBindPoint<T>.Remove(IBindable<T> bindable)
        {
            if (!_bindables.Contains(bindable))
                throw new FastenUpException("Bindable not found in bind point.");

            _bindables.Remove(bindable);
            
            if (bindable is IGettableBindable<T> bindableGettable)
                bindableGettable.OnBindableChanged -= OnValueChangedHandler;
        }

        private void OnValueChangedHandler(IBindable bindable)
        {
            if (bindable is IGettableBindable<T> bindableT)
                ChangeValue(bindableT.GetValue());
        }

        private void ChangeValue(T value)
        {
            _value = value;
            OnValueChanged?.Invoke(value);
        }
    }
}