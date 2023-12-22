using FastenUp.Runtime.Binders;
using UnityEngine.Events;

namespace FastenUp.Runtime.Base
{
    public sealed class BindAction<T> : BaseBindAction<UnityAction<T>>, IInternalBindAction<UnityAction<T>>
    {
        /// <inheritdoc />
        void IInternalBindAction<UnityAction<T>>.AddListener(IEventBinder<UnityAction<T>> listener)
        {
            AddBindableListener(listener);
        }

        /// <inheritdoc />
        void IInternalBindAction<UnityAction<T>>.RemoveListener(IEventBinder<UnityAction<T>> listener)
        {
            RemoveBindableListener(listener);
        }
    }

    public sealed class BindAction : BaseBindAction<UnityAction>, IInternalBindAction<UnityAction>
    {
        /// <inheritdoc />
        void IInternalBindAction<UnityAction>.AddListener(IEventBinder<UnityAction> listener)
        {
            AddBindableListener(listener);
        }

        /// <inheritdoc />
        void IInternalBindAction<UnityAction>.RemoveListener(IEventBinder<UnityAction> listener)
        {
            RemoveBindableListener(listener);
        }
    }
}