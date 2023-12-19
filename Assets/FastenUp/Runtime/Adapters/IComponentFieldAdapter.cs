namespace FastenUp.Runtime.Adapters
{
    /// <summary>
    /// A component field adapter is a class that wraps a component and exposes a single field of that component.
    /// It is used to abstract away the differences between components that have the same field but different types.
    /// </summary>
    internal interface IComponentFieldAdapter<T>
    {
        T Value { get; set; }
    }
}