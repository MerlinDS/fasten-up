using FastenUp.Runtime.Binders;

namespace FastenUp.Runtime.Mediators
{
    /// <summary>
    /// The IMediator interface is the base interface for ViewModels.
    /// It is a marker for Binders that the class contains bindable properties.
    /// </summary>
    /// <remarks>
    /// For the sake of code generation, the implementation of this interface must marked as partial.
    /// </remarks>
    /// <example>
    /// This example shows how to implement a mediator that can be used to bind values.
    /// <code>
    /// public partial class MyMediator : MonoBehaviour, IMediator
    /// {
    ///     public Bindable{string} Text = new ();
    ///     public BindableEvent OnClick = new ();
    ///
    ///     private void Awake()
    ///     {
    ///         Text.Value = "Hello World!";
    ///         OnClick.AddListener(() => Debug.Log("Clicked!"));
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="IBinder"/>
    public interface IMediator
    {
    }
}