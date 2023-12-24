namespace FastenUp.Runtime.Binders
{
    /// <summary>
    /// Binder that can receive a value.
    /// </summary>
    /// <typeparam name="T">Type of the value that will be set by the binder component</typeparam>
    public interface IValueReceiver<T> : IBinder<T>
    {
        /// <summary>
        /// Sets the value to the binder.
        /// </summary>
        /// <param name="value">The value that will be set to the binder.</param>
        void SetValue(T value);
    }
}