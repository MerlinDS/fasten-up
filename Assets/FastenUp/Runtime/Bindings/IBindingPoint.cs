namespace FastenUp.Runtime.Bindings
{
    public interface IBindingPoint<T> : IBindingPoint
    {
        T Value { get; set; }
    }
    
    public interface IBindingPoint
    {
        IBindingPoint<T> As<T>();
    }
}