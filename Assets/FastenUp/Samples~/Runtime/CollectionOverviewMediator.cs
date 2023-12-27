using System;
using System.Linq;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Mediators;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    internal partial class CollectionOverviewMediator : MonoBehaviour, IMediator
    {
        public Bindable<bool> Visibility { get; } = new();

        private BindableEvent AddNewItem { get; } = new();

        private BindableEvent RemoveLastItem { get; } = new();
        private BindableCollection<CollectionItemMediator> Collection { get; } = new();

        private int _counter;

        private void Awake()
        {
            for (var i = 0; i < 3; i++)
                AddItem();

            Collection.OnItemAdded += OnItemAdded;
            Collection.OnItemRemoved += OnRemoveItem;
            AddNewItem.AddListener(AddItem);
            RemoveLastItem.AddListener(RemoveItem);
        }

        private void OnDestroy()
        {
            AddNewItem.RemoveListener(AddItem);
            RemoveLastItem.RemoveListener(RemoveItem);
            Collection.OnItemAdded -= OnItemAdded;
            Collection.OnItemRemoved -= OnRemoveItem;
        }

        private void AddItem()
        {
            var item = new CollectionItemMediator(_counter++);
            item.OnRemove += i => Collection.Remove(i);
            Collection.Add(item);
        }

        private void RemoveItem()
        {
            var item = Collection.LastOrDefault();
            if (item is null)
                return;

            Collection.Remove(item);
        }

        private void OnItemAdded(CollectionItemMediator item)
        {
            if (Collection.Count > 5)
                AddNewItem.RemoveListener(AddItem);
            if (Collection.Count > 0 && !RemoveLastItem.HasListeners(RemoveItem))
                RemoveLastItem.AddListener(RemoveItem);
        }

        private void OnRemoveItem(CollectionItemMediator item)
        {
            Collection.Remove(item);
            if (Collection.Count <= 5 && !AddNewItem.HasListeners(AddItem))
                AddNewItem.AddListener(AddItem);
            if (Collection.Count == 0)
                RemoveLastItem.RemoveListener(RemoveItem);
        }
    }

    internal partial class CollectionItemMediator : IMediator, IDisposable
    {
        private Bindable<int> Index { get; } = new();
        private BindableEvent RemoveEvent { get; } = new();

        public Action<CollectionItemMediator> OnRemove;

        public CollectionItemMediator(int index)
        {
            Index.Value = index;
            RemoveEvent.AddListener(Remove);
        }

        private void Remove()
        {
            OnRemove?.Invoke(this);
        }

        public void Dispose()
        {
            RemoveEvent.RemoveListener(Remove);
            OnRemove = null;
        }
    }
}