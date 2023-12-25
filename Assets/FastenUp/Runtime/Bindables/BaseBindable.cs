using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Exceptions;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Base class for the bindable.
    /// </summary>
    /// <typeparam name="T">Type of the value</typeparam>
    public abstract class BaseBindable<T> : IBindable<T>
    {
        private readonly HashSet<IBinder<T>> _binders = new(1);

        private T _value;

        protected BaseBindable(T value)
        {
            _value = value;
        }
        
        /// <summary>
        /// The value that will be bind to the Unity components.
        /// </summary>
        public virtual T Value
        {
            get => _value;
            set
            {
                UpdateBinders(value);
                _value = value;
            }
        }

        void IBindable<T>.Bind(IBinder<T> binder)
        {
            if (_binders.Contains(binder))
                throw new FastenUpException($"{nameof(binder)} already bind to the {nameof(Bindable<T>)}.");

            if(binder is IValueReceiver<T> valueReceiver)
                valueReceiver.SetValue(_value);
            
            _binders.Add(binder);
            PostBind(binder);
        }

        void IBindable<T>.Unbind(IBinder<T> binder)
        {
            if (!_binders.Contains(binder))
                throw new FastenUpException($"{nameof(binder)} not bind to the {nameof(Bindable<T>)}.");

            _binders.Remove(binder);
            PostUnbind(binder);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void UpdateBinders(T value, IBinder<T> ignored = null)
        {
            foreach (var bindable in _binders)
            {
                if (bindable == ignored || bindable is not IValueReceiver<T> valueReceiver)
                    continue;

                valueReceiver.SetValue(value);
            }
        }
        
        protected void SetValueSilently(T value)
        {
            _value = value;
        }
        
        protected virtual void PostBind(IBinder<T> binder)
        {
            
        }
        
        protected virtual void PostUnbind(IBinder<T> binder)
        {
            
        }
    }
}