using FastenUp.Runtime.Delegates;
using FastenUp.Runtime.Mediators;

namespace FastenUp.Runtime.Binders
{
    /// <summary>
    /// Binder is a component that can be bind to the bindable property in the <see cref="IMediator"/>.
    /// </summary>
    public interface IBinder
    {
        /// <summary>
        /// The name of the bindable property in the <see cref="IMediator"/>.
        /// </summary>
        /// <remarks>
        /// If the name is empty, the binder will ignore the binding and log an error.
        /// To ignore the binding without logging an error, set the name that starts with the # symbol.
        /// </remarks>
        string Name { get; }
        
        /// <summary>
        /// The event will be invoked when the binder is changed somehow (For example, when the value is changed).
        /// Value in the event is the binder that was changed.
        /// </summary>
        public event OnBinderChanged OnBinderChanged;
    }

    /// <remarks>Provides one-way binding</remarks>
    /// <typeparam name="T">Type of the value that will be set by the binder component</typeparam>
    /// <inheritdoc cref="IBinder"/>
    public interface IBinder<in T> : IBinder
    {
        void SetValue(T value);
    }
}