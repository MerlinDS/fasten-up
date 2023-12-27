using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Mediators;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    public sealed partial class ComponentsOverviewMediator : MonoBehaviour, IMediator
    {
        private BindableRef<TextOverviewMediator> TextOverview { get; } = new();
        private BindableRef<GraphicsOverviewMediator> GraphicsOverview { get; } = new();
        private BindableRef<InteractiveOverviewMediator> InteractiveOverview { get; } = new();
        private BindableRef<EventsOverviewMediator> EventsOverview { get; } = new();
        
        private BindableRef<ActionsOverviewMediator> ActionsOverview { get; } = new();
        
        private BindableRef<CollectionOverviewMediator> CollectionOverview { get; } = new();

        private BindableEvent TextOverviewButton { get; } = new();
        private BindableEvent GraphicsOverviewButton { get; } = new();
        private BindableEvent InteractiveOverviewButton { get; } = new();
        private BindableEvent EventsOverviewButton { get; } = new();
        
        private BindableEvent ActionsOverviewButton { get; } = new();
        
        private BindableEvent CollectionOverviewButton { get; } = new();
        

        private void Awake()
        {
            if (TextOverview.Value != null)
                OnTextOverviewButtonClick();
        }

        private void OnDestroy()
        {
            if (TextOverviewButton.HasListeners(OnTextOverviewButtonClick))
                TextOverviewButton.RemoveListener(OnTextOverviewButtonClick);
            if (GraphicsOverviewButton.HasListeners(OnGraphicsOverviewButtonClick))
                GraphicsOverviewButton.RemoveListener(OnGraphicsOverviewButtonClick);
            if (InteractiveOverviewButton.HasListeners(OnInteractiveOverviewButtonClick))
                InteractiveOverviewButton.RemoveListener(OnInteractiveOverviewButtonClick);
            if (EventsOverviewButton.HasListeners(OnEventsOverviewButtonClick))
                EventsOverviewButton.RemoveListener(OnEventsOverviewButtonClick);
            if (CollectionOverviewButton.HasListeners(OnCollectionOverviewButtonClick))
                CollectionOverviewButton.RemoveListener(OnCollectionOverviewButtonClick);
            if(ActionsOverviewButton.HasListeners(OnActionsOverviewButtonClick))
                ActionsOverviewButton.RemoveListener(OnActionsOverviewButtonClick);
        }

        private void OnTextOverviewButtonClick()
        {
            SetVisible(TextOverview.Value);
            if (TextOverviewButton.HasListeners(OnTextOverviewButtonClick))
                TextOverviewButton.RemoveListener(OnTextOverviewButtonClick);
            if(!GraphicsOverviewButton.HasListeners(OnGraphicsOverviewButtonClick))
                GraphicsOverviewButton.AddListener(OnGraphicsOverviewButtonClick);
            if(!InteractiveOverviewButton.HasListeners(OnInteractiveOverviewButtonClick))
                InteractiveOverviewButton.AddListener(OnInteractiveOverviewButtonClick);
            if(!EventsOverviewButton.HasListeners(OnEventsOverviewButtonClick))
                EventsOverviewButton.AddListener(OnEventsOverviewButtonClick);
            if(!CollectionOverviewButton.HasListeners(OnCollectionOverviewButtonClick))
                CollectionOverviewButton.AddListener(OnCollectionOverviewButtonClick);
            if(!ActionsOverviewButton.HasListeners(OnActionsOverviewButtonClick))
                ActionsOverviewButton.AddListener(OnActionsOverviewButtonClick);
        }

        private void OnGraphicsOverviewButtonClick()
        {
            SetVisible(GraphicsOverview.Value);
            if (GraphicsOverviewButton.HasListeners(OnGraphicsOverviewButtonClick))
                GraphicsOverviewButton.RemoveListener(OnGraphicsOverviewButtonClick);
            if(!TextOverviewButton.HasListeners(OnTextOverviewButtonClick))
                TextOverviewButton.AddListener(OnTextOverviewButtonClick);
            if(!InteractiveOverviewButton.HasListeners(OnInteractiveOverviewButtonClick))
                InteractiveOverviewButton.AddListener(OnInteractiveOverviewButtonClick);
            if(!EventsOverviewButton.HasListeners(OnEventsOverviewButtonClick))
                EventsOverviewButton.AddListener(OnEventsOverviewButtonClick);
            if(!CollectionOverviewButton.HasListeners(OnCollectionOverviewButtonClick))
                CollectionOverviewButton.AddListener(OnCollectionOverviewButtonClick);
            if(!ActionsOverviewButton.HasListeners(OnActionsOverviewButtonClick))
                ActionsOverviewButton.AddListener(OnActionsOverviewButtonClick);
        }

        private void OnInteractiveOverviewButtonClick()
        {
            SetVisible(InteractiveOverview.Value);
            if (InteractiveOverviewButton.HasListeners(OnInteractiveOverviewButtonClick))
                InteractiveOverviewButton.RemoveListener(OnInteractiveOverviewButtonClick);
            if(!TextOverviewButton.HasListeners(OnTextOverviewButtonClick))
                TextOverviewButton.AddListener(OnTextOverviewButtonClick);
            if(!GraphicsOverviewButton.HasListeners(OnGraphicsOverviewButtonClick))
                GraphicsOverviewButton.AddListener(OnGraphicsOverviewButtonClick);
            if(!EventsOverviewButton.HasListeners(OnEventsOverviewButtonClick))
                EventsOverviewButton.AddListener(OnEventsOverviewButtonClick);
            if(!CollectionOverviewButton.HasListeners(OnCollectionOverviewButtonClick))
                CollectionOverviewButton.AddListener(OnCollectionOverviewButtonClick);
            if(!ActionsOverviewButton.HasListeners(OnActionsOverviewButtonClick))
                ActionsOverviewButton.AddListener(OnActionsOverviewButtonClick);
        }


        private void OnEventsOverviewButtonClick()
        {
            SetVisible(EventsOverview.Value);
            if (EventsOverviewButton.HasListeners(OnEventsOverviewButtonClick))
                EventsOverviewButton.RemoveListener(OnEventsOverviewButtonClick);
            if(!TextOverviewButton.HasListeners(OnTextOverviewButtonClick))
                TextOverviewButton.AddListener(OnTextOverviewButtonClick);
            if(!GraphicsOverviewButton.HasListeners(OnGraphicsOverviewButtonClick))
                GraphicsOverviewButton.AddListener(OnGraphicsOverviewButtonClick);
            if(!InteractiveOverviewButton.HasListeners(OnInteractiveOverviewButtonClick))
                InteractiveOverviewButton.AddListener(OnInteractiveOverviewButtonClick);
            if(!CollectionOverviewButton.HasListeners(OnCollectionOverviewButtonClick))
                CollectionOverviewButton.AddListener(OnCollectionOverviewButtonClick);
            if(!ActionsOverviewButton.HasListeners(OnActionsOverviewButtonClick))
                ActionsOverviewButton.AddListener(OnActionsOverviewButtonClick);
        }

        private void OnActionsOverviewButtonClick()
        {
            SetVisible(ActionsOverview.Value);
            if (ActionsOverviewButton.HasListeners(OnActionsOverviewButtonClick))
                ActionsOverviewButton.RemoveListener(OnActionsOverviewButtonClick);
            if(!TextOverviewButton.HasListeners(OnTextOverviewButtonClick))
                TextOverviewButton.AddListener(OnTextOverviewButtonClick);
            if(!GraphicsOverviewButton.HasListeners(OnGraphicsOverviewButtonClick))
                GraphicsOverviewButton.AddListener(OnGraphicsOverviewButtonClick);
            if(!InteractiveOverviewButton.HasListeners(OnInteractiveOverviewButtonClick))
                InteractiveOverviewButton.AddListener(OnInteractiveOverviewButtonClick);
            if(!EventsOverviewButton.HasListeners(OnEventsOverviewButtonClick))
                EventsOverviewButton.AddListener(OnEventsOverviewButtonClick);
            if(!CollectionOverviewButton.HasListeners(OnCollectionOverviewButtonClick))
                CollectionOverviewButton.AddListener(OnCollectionOverviewButtonClick);
        }
        
        private void OnCollectionOverviewButtonClick()
        {
            SetVisible(CollectionOverview.Value);
            if (CollectionOverviewButton.HasListeners(OnCollectionOverviewButtonClick))
                CollectionOverviewButton.RemoveListener(OnCollectionOverviewButtonClick);
            if(!TextOverviewButton.HasListeners(OnTextOverviewButtonClick))
                TextOverviewButton.AddListener(OnTextOverviewButtonClick);
            if(!GraphicsOverviewButton.HasListeners(OnGraphicsOverviewButtonClick))
                GraphicsOverviewButton.AddListener(OnGraphicsOverviewButtonClick);
            if(!InteractiveOverviewButton.HasListeners(OnInteractiveOverviewButtonClick))
                InteractiveOverviewButton.AddListener(OnInteractiveOverviewButtonClick);
            if(!EventsOverviewButton.HasListeners(OnEventsOverviewButtonClick))
                EventsOverviewButton.AddListener(OnEventsOverviewButtonClick);
            if(!ActionsOverviewButton.HasListeners(OnActionsOverviewButtonClick))
                ActionsOverviewButton.AddListener(OnActionsOverviewButtonClick);
        }

        private void SetVisible(IMediator mediator)
        {
            TextOverview.Value.Visibility.Value = ReferenceEquals(mediator, TextOverview.Value);
            GraphicsOverview.Value.Visibility.Value = ReferenceEquals(mediator, GraphicsOverview.Value);
            InteractiveOverview.Value.Visibility.Value = ReferenceEquals(mediator, InteractiveOverview.Value);
            EventsOverview.Value.Visibility.Value = ReferenceEquals(mediator, EventsOverview.Value);
            ActionsOverview.Value.Visibility.Value = ReferenceEquals(mediator, ActionsOverview.Value);
            CollectionOverview.Value.Visibility.Value = ReferenceEquals(mediator, CollectionOverview.Value);
        }
    }
}