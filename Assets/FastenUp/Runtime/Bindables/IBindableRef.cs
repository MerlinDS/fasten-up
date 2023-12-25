namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Implementation of this interface provides the ability to bind object references to a
    /// <see cref="FastenUp.Runtime.Mediators.IMediator"/>.
    /// </summary>
    /// <typeparam name="T">Type of the reference</typeparam>
    public interface IBindableRef<in T> : IBindable
    {
        internal void Bind(T reference);

        internal void Unbind(T reference);
    }
}