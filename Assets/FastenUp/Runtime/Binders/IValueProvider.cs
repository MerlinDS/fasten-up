namespace FastenUp.Runtime.Binders
{
    /// <summary>
    /// Binder that can provide a value.
    /// </summary>
    /// <typeparam name="T">Type of the value that will be provided by the binder component</typeparam>
    public interface IValueProvider<T> : IValueReceiver<T>, IBinder<T>
    {
        /// <summary>
        /// Gets the value from the binder.
        /// </summary>
        /// <returns>The value that was provided by the binder.</returns>
        T GetValue();
    }
}