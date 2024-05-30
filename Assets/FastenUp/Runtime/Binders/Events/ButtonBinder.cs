using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FastenUp.Runtime.Binders.Events
{
    /// <summary>
    /// The one-way <see cref="IBinder"/> binds a value to the component with <see cref="UnityAction"/> field.
    /// </summary>
    [RequireComponent(typeof(Button))]
    [AddComponentMenu(FastenUpComponentMenu.BaseMenu + "Button Binder", 0)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Binders#button")]
    public class ButtonBinder : BaseBinder, IEventBinder<UnityAction>
    {
        private Button _component;
        private int _listenerCount;

        private void Awake()
        {
            _component = GetComponent<Button>();
            _component.interactable = false;
        }

        /// <inheritdoc />
        public void AddListener(UnityAction action)
        {
            if (action is null)
            {
                return;
            }

            _component.onClick.AddListener(action);
            _component.interactable = ++_listenerCount > 0;
        }

        /// <inheritdoc />
        public void RemoveListener(UnityAction action)
        {
            if (action is null)
            {
                return;
            }

            _component.onClick.RemoveListener(action);
            _component.interactable = --_listenerCount > 0;
        }

        /// <inheritdoc />
        protected override void OnDisable()
        {
            base.OnDisable();
            _component.onClick.RemoveAllListeners();
        }
    }
}