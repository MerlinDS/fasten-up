namespace FastenUp.Runtime.ComponentManagement
{
    public interface IComponentManager
    {
        /// <summary>
        /// Sets the value to the component attached to the game object
        /// </summary>
        /// <typeparam name="T">The type of the component.</typeparam>
        /// <param name="value">The value to set.</param>
        void SetValue<T>(T value);
        /// <summary>
        /// Gets the value from the component attached to the game object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetValue<T>();
    }
}