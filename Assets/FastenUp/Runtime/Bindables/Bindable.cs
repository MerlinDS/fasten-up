using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Exceptions;

namespace FastenUp.Runtime.Bindables
{
    public sealed class Bindable<T> : IBindable<T>, IInternalBindable<T>
    {
        private readonly HashSet<IBinder<T>> _binders = new(1);

        private T _value;

        public Bindable(T value = default)
        {
            _value = value;
        }

        public T Value
        {
            get => _value;
            set
            {
                UpdateBinders(value);
                ChangeValueAndNotify(value);
            }
        }

        public event Action<T> OnValueChanged;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IInternalBindable<T>.Bind(IBinder<T> binder)
        {
            if (_binders.Contains(binder))
                throw new FastenUpException($"{nameof(binder)} already bind to the {nameof(Bindable<T>)}.");

            binder.SetValue(_value);
            _binders.Add(binder);

            if (binder is IGettableBinder<T> bindableGettable)
                bindableGettable.OnBinderChanged += OnValueChangedHandler;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IInternalBindable<T>.Unbind(IBinder<T> binder)
        {
            if (!_binders.Contains(binder))
                throw new FastenUpException($"{nameof(binder)} not bind to the {nameof(Bindable<T>)}.");

            _binders.Remove(binder);
            
            if (binder is IGettableBinder<T> bindableGettable)
                bindableGettable.OnBinderChanged -= OnValueChangedHandler;
        }

        private void OnValueChangedHandler(IBinder binder)
        {
            if (binder is not IGettableBinder<T> bindableT)
                return;

            var value = bindableT.GetValue();
            UpdateBinders(value, bindableT);
            ChangeValueAndNotify(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateBinders(T value, IBinder<T> ignored = null)
        {
            foreach (var bindable in _binders)
            {
                if (bindable == ignored)
                    continue;

                bindable.SetValue(value);
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