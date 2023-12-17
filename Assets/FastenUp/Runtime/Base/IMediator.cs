namespace FastenUp.Runtime.Base
{
    /// <summary>
    /// Mark implementation as a mediator that can be used to bind unity components to bind points.
    /// </summary>
    /// <remarks>
    /// The implementation must be marked as partial.
    /// This needs to be done for the code generator to work.
    /// </remarks>
    /// <example>
    /// This example shows how to implement a mediator that can be used to bind values.
    /// FastenUp will find <see cref="FastenUp.Runtime.Bindables.IBindable"/> in children and bind them to the bind points automatically.
    /// <code>
    /// public partial class MyMediator : MonoBehaviour, IMediator
    /// {
    ///     public BindPoint{string} Text = new ();
    ///     public BindPoint{UnityEvent} OnClick = new ();
    ///
    ///     private void Awake()
    ///     {
    ///         Text.Value = "Hello World!";
    ///         OnClick.Value.AddListener(() => Debug.Log("Clicked!"));
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="FastenUp.Runtime.Base.IBindPoint{T}"/>
    public interface IMediator
    {
    }
}