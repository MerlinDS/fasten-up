namespace FastenUp.Runtime.Bindings
{
    public interface IBindingPoint<T> : IBindingPoint
    {
        T Value { get; set; }
    }
    
    public interface IBindingPoint
    {
        string Name { get; }
        IBindingPoint<T> As<T>();
        
        bool CanBind<T>();
    }
}