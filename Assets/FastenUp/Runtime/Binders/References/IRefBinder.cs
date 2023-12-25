namespace FastenUp.Runtime.Binders.References
{
    /// <summary>
    /// Implementation of this interface provides the ability to bind object references to a
    /// <see cref="FastenUp.Runtime.Mediators.IMediator"/>
    /// </summary>
    public interface IRefBinder : IBinder
    {
        /// <summary>
        /// Tries to get the reference of type <typeparamref name="T"/> from the binder.
        /// </summary>
        /// <param name="reference"> The reference of type <typeparamref name="T"/> if found, otherwise null.</param>
        /// <typeparam name="T">Type of the reference</typeparam>
        /// <returns>True if the reference was found, otherwise false.</returns>
        bool TryGetReference<T>(out T reference);
    }
}