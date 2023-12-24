using UnityEngine.Events;

namespace FastenUp.Runtime.Bindables
{
    public sealed class BindableEvent<T> : BaseBindableEvent<UnityAction<T>>
    {
    }

    public sealed class BindableEvent : BaseBindableEvent<UnityAction>
    {
    }
}