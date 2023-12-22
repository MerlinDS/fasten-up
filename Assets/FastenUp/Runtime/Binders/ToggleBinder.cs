using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace FastenUp.Runtime.Binders
{
    /// <summary>
    /// The two-way <see cref="IBinder"/> binds a value to the component with <see cref="Toggle"/> component.
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    [AddComponentMenu(FastenUpComponentMenu.BaseMenu + "Toggle Binder" , 4)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Binders#toggle")]
    public sealed class ToggleBinder : BaseBinder, IGettableBinder<bool>
    {
        private Toggle _toggle;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(OnValueChanged);
        }

        /// <inheritdoc />
        protected override void OnDisable()
        {
            base.OnDisable();
            _toggle.onValueChanged.RemoveListener(OnValueChanged);
        }

        /// <inheritdoc />
        public void SetValue(bool value)
        {
            _toggle.SetIsOnWithoutNotify(value);
        }

        /// <inheritdoc />
        public bool GetValue()
        {
            return _toggle.isOn;
        }

        private void OnValueChanged(bool value) =>
            InvokeOnBinderChanged();
    }
}