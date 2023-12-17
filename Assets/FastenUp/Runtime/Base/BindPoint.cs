using System;
using System.Collections.Generic;
using FastenUp.Runtime.Bindables;

namespace FastenUp.Runtime.Base
{   
    public sealed class BindPoint<T>
    {
        private List<IBindable> _bindables;
        private T _value;
        
        public T Value
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public event Action<T> OnValueChanged;

        internal void Add(IBindable bindable)
        {
            bindable.OnValueChanged += OnValueChangedHandler;
            throw new NotImplementedException();
        }
        
        internal void Remove(IBindable bindable)
        {
            throw new NotImplementedException();
        }
        
        private void OnValueChangedHandler(IBindable bindable, Type valueType)
        {
            _value = ((IBindable<T>) bindable).GetValue();
            NotifyValueChanged();
            throw new NotImplementedException();
        }
        
        private void NotifyValueChanged()
        {
            OnValueChanged?.Invoke(_value);
        }
    }
}