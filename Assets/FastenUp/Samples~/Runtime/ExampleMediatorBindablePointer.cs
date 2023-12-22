using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Mediators;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace FastenUp.Examples.Runtime
{
    internal partial class ExampleMediatorBindablePointer : MonoBehaviour, IMediator
    {
        private BindableEvent<PointerEventData> OnClick { get; } = new();
        private BindableEvent<PointerEventData> OnEnter { get; } = new();
        private BindableEvent<PointerEventData> OnExit { get; } = new();
        
        private BindableEvent<PointerEventData> OnDown { get; } = new();
        
        private BindableEvent<PointerEventData> OnUp { get; } = new();
        
        private BindableEvent<PointerEventData> OnMove { get; } = new();
        
        private Bindable<string> PointerAction { get; } = new();
        
        private void Awake()
        {
            OnClick.AddListener(OnClickHandler);
            OnEnter.AddListener(OnEnterHandler);
            OnExit.AddListener(OnExitHandler);
        }
        
        public void OnClickChangeSubscription()
        {
            SwitchListener(OnClick, OnClickHandler);
        }

        public void OnEnterChangeSubscription()
        {
            SwitchListener(OnEnter, OnEnterHandler);
        }
        
        public void OnExitChangeSubscription()
        {
            SwitchListener(OnExit, OnExitHandler);
        }
        
        public void OnDownChangeSubscription()
        {
            SwitchListener(OnDown, OnDownHandler);
        }
        
        public void OnUpChangeSubscription()
        {
            SwitchListener(OnUp, OnUpHandler);
        }
        
        public void OnMoveChangeSubscription()
        {
            SwitchListener(OnMove, OnMoveHandler);
        }
        
        private static void SwitchListener(BindableEvent<PointerEventData> bind, UnityAction<PointerEventData> action)
        {
            if (!bind.HasListeners(action))
            {
                bind.AddListener(action);
                return;
            }

            bind.RemoveListener(action);
        }
        
        private void OnClickHandler(PointerEventData eventData)
        {
            PointerAction.Value = $"Clicked at {eventData.position}";
        }
        
        private void OnEnterHandler(PointerEventData eventData)
        {
            PointerAction.Value = $"Entered at {eventData.position}";
        }
        
        private void OnExitHandler(PointerEventData eventData)
        {
            PointerAction.Value = $"Exited at {eventData.position}";
        }
        
        private void OnDownHandler(PointerEventData eventData)
        {
            PointerAction.Value = $"Down at {eventData.position}";
        }
        
        private void OnUpHandler(PointerEventData eventData)
        {
            PointerAction.Value = $"Up at {eventData.position}";
        }
        
        private void OnMoveHandler(PointerEventData eventData)
        {
            PointerAction.Value = $"Moved at {eventData.position}";
        }
        
        
        private void OnDestroy()
        {
            OnClick.Dispose();
            OnEnter.Dispose();
            OnExit.Dispose();
            OnDown.Dispose();
            OnUp.Dispose();
            OnMove.Dispose();
        }
    }
}