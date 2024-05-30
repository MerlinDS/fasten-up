using System.Runtime.CompilerServices;
using FastenUp.Runtime.Binders;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Base class for the bindable.
    /// </summary>
    /// <typeparam name="T">Type of the value</typeparam>
    public abstract class BaseBindable<T> : IBindable<T>
    {
        private readonly BinderSet<IBinder<T>> _binders = new();

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
            _binders.Add(binder);
            if (binder is IValueReceiver<T> valueReceiver)
            {
                valueReceiver.SetValue(_value);
            }

            PostBind(binder);
        }

        void IBindable<T>.Unbind(IBinder<T> binder)
        {
            _binders.Remove(binder);
            PostUnbind(binder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void UpdateBinders(T value, IBinder<T> ignored = null)
        {
            foreach (IBinder<T> bindable in _binders)
            {
                if (bindable == ignored || bindable is not IValueReceiver<T> valueReceiver)
                {
                    continue;
                }

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