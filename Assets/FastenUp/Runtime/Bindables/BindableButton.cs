using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// The one-way <see cref="IBindable"/> binds a value to the component with <see cref="UnityAction"/> field.
    /// </summary>
    [RequireComponent(typeof(Button))]
    [AddComponentMenu(FastenUpComponentMenu.BaseMenu + "Bindable Button", 0)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Core-Functionalities#bindable-button")]
    public class BindableButton : BaseBindable, IBindableListener<UnityAction>
    {
        private Button _component;

        private void Awake()
        {
            _component = GetComponent<Button>();
        }

        /// <inheritdoc />
        public void AddListener(UnityAction action)
        {
            if (action is not null)
                _component.onClick.AddListener(action);
        }

        /// <inheritdoc />
        public void RemoveListener(UnityAction action)
        {
            if (action is not null)
                _component.onClick.RemoveListener(action);
        }

        /// <inheritdoc />
        protected override void OnDisable()
        {
            base.OnDisable();
            _component.onClick.RemoveAllListeners();
        }
    }
}