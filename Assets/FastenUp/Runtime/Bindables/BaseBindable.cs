using System;
using System.Diagnostics.CodeAnalysis;
using FastenUp.Runtime.Base;
using FastenUp.Runtime.Delegates;
using UnityEngine;

namespace FastenUp.Runtime.Bindables
{
    public abstract class BaseBindable : MonoBehaviour, IBindable
    {
        private IInternalMediator _mediator;

        [field: SerializeField] public string Name { get; [ExcludeFromCodeCoverage] private set; }


        /// <inheritdoc />
        public event OnValueChanged OnValueChanged;

        protected void InvokeValueChanged<TValue>() =>
            OnValueChanged?.Invoke(this, typeof(TValue));

        private void OnEnable()
        {
            //TODO: Validate name
            _mediator ??=
                GetComponentInParent<IInternalMediator>(); //Cache mediator to avoid GetComponentInParent calls
            if (_mediator is null)
                throw new Exception("No mediator found"); //TODO: Change exception type to custom
            _mediator.Bind(this);
        }

        private void OnDisable()
        {
            _mediator?.Unbind(this);
            OnValueChanged = null;
        }
    }
}