namespace FastenUp.Runtime.Binders
{
    /// <remarks>Provides two-way binding</remarks>
    /// <typeparam name="T">Type of the value that will be set or get by the bind component</typeparam>
    /// <inheritdoc cref="IBinder"/>
    public interface IGettableBinder<T> : IBinder<T>
    {
        T GetValue();
    }
}