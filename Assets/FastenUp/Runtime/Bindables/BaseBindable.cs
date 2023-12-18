using System;
using System.Diagnostics.CodeAnalysis;
using FastenUp.Runtime.Base;
using FastenUp.Runtime.Delegates;
using FastenUp.Runtime.Exceptions;
using UnityEngine;

namespace FastenUp.Runtime.Bindables
{
    [Serializable]
    public abstract class BaseBindable : MonoBehaviour, IBindable
    {
        private IInternalMediator _mediator;

        [field: SerializeField] public string Name { get; [ExcludeFromCodeCoverage] private set; }

        public event OnBindableChanged OnBindableChanged;

        protected void InvokeOnBindableChanged() =>
            OnBindableChanged?.Invoke(this);

        protected virtual void OnEnable()
        {
            ValidateName();
            CacheMediator();
            _mediator.Bind(this);
        }

        protected virtual void OnDisable()
        {
            _mediator?.Unbind(this);
        }
        
        private void ValidateName()
        {
            if (string.IsNullOrEmpty(Name))
               throw new FastenUpBindableException("Bindable name is null or empty.", gameObject);
        }

        private void CacheMediator()
        {
            _mediator ??= GetComponentInParent<IInternalMediator>();
            if (_mediator is null)
                throw  new FastenUpBindableException("Mediator not found", gameObject);
        }
    }
}