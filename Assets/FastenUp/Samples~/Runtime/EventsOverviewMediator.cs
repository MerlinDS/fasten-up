using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Mediators;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastenUp.Examples.Runtime
{
    internal partial class EventsOverviewMediator : MonoBehaviour, IMediator
    {
        public Bindable<bool> Visibility { get; } = new();
        private BindableEvent Button { get; } = new();

        private Bindable<int> PressedCount { get; } = new();
        
        private BindableEvent<PointerEventData> PointerClick { get; } = new();
        private Bindable<string> PositionClick { get; } = new();
        
        private BindableEvent<PointerEventData> PointerEnter { get; } = new();
        private Bindable<string> PositionEnter { get; } = new();
        
        private BindableEvent<PointerEventData> PointerExit { get; } = new();
        private Bindable<string> PositionExit { get; } = new();
        
        private BindableEvent<PointerEventData> PointerDown { get; } = new();
        private Bindable<string> PositionDown { get; } = new();
        
        private BindableEvent<PointerEventData> PointerUp { get; } = new();
        private Bindable<string> PositionUp { get; } = new();
        
        private BindableEvent<PointerEventData> PointerMove { get; } = new();
        private Bindable<string> PositionMove { get; } = new();

        private void Awake()
        {
            Button.AddListener(OnButtonClick);
            PointerClick.AddListener(OnPointerClick);
            PointerEnter.AddListener(OnPointerEnter);
            PointerExit.AddListener(OnPointerExit);
            PointerDown.AddListener(OnPointerDown);
            PointerUp.AddListener(OnPointerUp);
            PointerMove.AddListener(OnPointerMove);
        }

        private void OnDestroy()
        {
            Button.Dispose();
            PointerClick.Dispose();
            PointerEnter.Dispose();
            PointerExit.Dispose();
            PointerDown.Dispose();
            PointerUp.Dispose();
            PointerMove.Dispose();
        }

        private void OnButtonClick()
        {
            PressedCount.Value++;
        }
        
        private void OnPointerClick(PointerEventData eventData)
        {
            PositionClick.Value = eventData.position.ToString();
        }
        
        private void OnPointerEnter(PointerEventData eventData)
        {
            PositionEnter.Value = eventData.position.ToString();
        }
        
        private void OnPointerExit(PointerEventData eventData)
        {
            PositionExit.Value = eventData.position.ToString();
        }
        
        private void OnPointerDown(PointerEventData eventData)
        {
            PositionDown.Value = eventData.position.ToString();
        }
        
        private void OnPointerUp(PointerEventData eventData)
        {
            PositionUp.Value = eventData.position.ToString();
        }
        
        private void OnPointerMove(PointerEventData eventData)
        {
            PositionMove.Value = eventData.position.ToString();
        }
        
        
    }
}