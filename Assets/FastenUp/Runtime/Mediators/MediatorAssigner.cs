using System;
using System.Collections.Generic;
using FastenUp.Runtime.Binders;
using UnityEngine;

namespace FastenUp.Runtime.Mediators
{
    /// <summary>
    /// Used to bind external <see cref="IMediator"/> to the binders.
    /// </summary>
    public sealed class MediatorAssigner : MonoBehaviour, IInternalMediator
    {
        private readonly List<IBinder> _binders = new();
        
        private IInternalMediator _mediator;

        /// <summary>
        /// Binds external mediator to the binders.
        /// </summary>
        /// <param name="mediator">Object that implements <see cref="IMediator"/> interface.</param>
        /// <typeparam name="T">Type of the mediator.</typeparam>
        public void Assign<T>(T mediator) where T : IInternalMediator
        {
            if (mediator is null)
                throw new ArgumentNullException(nameof(mediator), "Mediator cannot be null.");
            
            Release();
            
            _mediator = mediator;
            foreach (var binder in _binders)
                _mediator.Bind(binder);
        }

        private void OnDestroy()
        {
            Release();
            _binders.Clear();
        }

        ///<summary>
        /// Unbinds all binders from the external mediator.
        /// </summary>
        public void Release()
        {
            if (_mediator is null)
                return;
            
            foreach (var binder in _binders)
                _mediator.Unbind(binder);
            
            if(_mediator is IDisposable disposable)
                disposable.Dispose();
            _mediator = null;
        }

        /// <inheritdoc />
        public void Bind(IBinder binder)
        {
            _binders.Add(binder);
            _mediator?.Bind(binder);
        }

        /// <inheritdoc />
        public void Unbind(IBinder binder)
        {
            _binders.Remove(binder);
            _mediator?.Unbind(binder);
        }
    }
}