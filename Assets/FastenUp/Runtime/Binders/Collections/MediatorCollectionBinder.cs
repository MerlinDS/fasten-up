using System;
using System.Collections.Generic;
using FastenUp.Runtime.Mediators;
using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.Pool;

namespace FastenUp.Runtime.Binders.Collections
{
    /// <summary>
    /// Binder that provides the ability to add and remove <see cref="IInternalMediator"/> to the bound collection.
    /// <see cref="FastenUp.Runtime.Bindables.BindableCollection{T}"/>
    /// </summary>
    [AddComponentMenu(FastenUpComponentMenu.Collections + "Mediator Collection Binder", 0)]
    // [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Collections#mediator-collection")]
    public sealed class MediatorCollectionBinder : BaseBinder, ICollectionBinder<IInternalMediator>
    {
        [SerializeField] private GameObject _prefab;

        private IObjectPool<MediatorAssigner> _pool;

        private IObjectPool<MediatorAssigner> Pool =>
            _pool ??= new ObjectPool<MediatorAssigner>(Create, OnGet, OnRelease);

        private readonly Dictionary<IInternalMediator, MediatorAssigner> _assigners = new();

        /// <inheritdoc />
        public void Add(IInternalMediator item)
        {
            MediatorAssigner assigner = Pool.Get();
            assigner.Assign(item);
            _assigners.Add(item, assigner);
        }

        /// <inheritdoc />
        public void Remove(IInternalMediator item)
        {
            if (!_assigners.TryGetValue(item, out MediatorAssigner assigner))
            {
                return;
            }

            Pool.Release(assigner);
            _assigners.Remove(item);
        }

        /// <inheritdoc />
        protected override void OnEnable()
        {
            if (_prefab == null)
            {
                throw new NullReferenceException("Prefab cannot be null.");
            }

            _prefab.SetActive(false);
            base.OnEnable();
        }

        /// <inheritdoc />
        protected override void OnDisable()
        {
            foreach (MediatorAssigner assigner in _assigners.Values)
            {
                Pool.Release(assigner);
            }

            _assigners.Clear();
            base.OnDisable();
        }

        private MediatorAssigner Create()
        {
            GameObject instance = Instantiate(_prefab, transform);
            instance.SetActive(false);

            bool hasAssigner = instance.TryGetComponent(out MediatorAssigner assigner);
            return hasAssigner ? assigner : instance.AddComponent<MediatorAssigner>();
        }

        private void OnGet(MediatorAssigner mediatorAssigner)
        {
            mediatorAssigner.gameObject.SetActive(true);
            mediatorAssigner.transform.SetSiblingIndex(transform.childCount - 1);
        }

        private static void OnRelease(MediatorAssigner mediatorAssigner)
        {
            mediatorAssigner.gameObject.SetActive(false);
            mediatorAssigner.Release();
        }
    }
}