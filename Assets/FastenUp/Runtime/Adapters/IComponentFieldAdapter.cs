namespace FastenUp.Runtime.Adapters
{
    public interface IComponentFieldAdapter<T>
    {
        T Value { get; set; }
    }
}