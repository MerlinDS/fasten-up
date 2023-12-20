using FastenUp.Runtime.Bindables;
using UnityEngine.Events;

namespace FastenUp.Runtime.Base
{
    public sealed class BindAction<T> : BaseBindAction<UnityAction<T>>, IInternalBindAction<UnityAction<T>>
    {
        /// <inheritdoc />
        void IInternalBindAction<UnityAction<T>>.AddListener(IBindableListener<UnityAction<T>> listener)
        {
            AddBindableListener(listener);
        }

        /// <inheritdoc />
        void IInternalBindAction<UnityAction<T>>.RemoveListener(IBindableListener<UnityAction<T>> listener)
        {
            RemoveBindableListener(listener);
        }
    }

    public sealed class BindAction : BaseBindAction<UnityAction>, IInternalBindAction<UnityAction>
    {
        /// <inheritdoc />
        void IInternalBindAction<UnityAction>.AddListener(IBindableListener<UnityAction> listener)
        {
            AddBindableListener(listener);
        }

        /// <inheritdoc />
        void IInternalBindAction<UnityAction>.RemoveListener(IBindableListener<UnityAction> listener)
        {
            RemoveBindableListener(listener);
        }
    }
}