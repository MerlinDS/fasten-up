namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Used to setup the binder during runtime.
    /// </summary>
    /// <typeparam name="T">Type of the setup value</typeparam>
    public sealed class BindableSetup<T> : BaseBindable<T>
    {
        /// <inheritdoc />
        public BindableSetup(T value = default) : base(value)
        {
        }
    }
}