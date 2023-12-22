using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace FastenUp.Runtime.Binders.Pointers
{
    public abstract class PointerBinder : BaseBinder,
        IEventBinder<UnityAction>,
        IEventBinder<UnityAction<PointerEventData>>
    {
        private readonly UnityEvent _event = new();
        private readonly UnityEvent<PointerEventData> _eventWithData = new();

        protected void OnPointerEvent(PointerEventData eventData)
        {
            _eventWithData.Invoke(eventData);
            _event.Invoke();
        }

        /// <inheritdoc />
        public void AddListener(UnityAction action)
        {
            if (action is not null)
                _event.AddListener(action);
        }

        /// <inheritdoc />
        public void RemoveListener(UnityAction action)
        {
            if (action is not null)
                _event.RemoveListener(action);
        }

        /// <inheritdoc />
        public void AddListener(UnityAction<PointerEventData> action)
        {
            if (action is not null)
                _eventWithData.AddListener(action);
        }

        /// <inheritdoc />
        public void RemoveListener(UnityAction<PointerEventData> action)
        {
            if (action is not null)
                _eventWithData.RemoveListener(action);
        }

        /// <inheritdoc />
        protected override void OnDisable()
        {
            base.OnDisable();
            _event.RemoveAllListeners();
            _eventWithData.RemoveAllListeners();
        }
    }
}