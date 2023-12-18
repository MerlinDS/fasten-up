using UnityEngine;
using UnityEngine.UI;

namespace FastenUp.Runtime.Bindables
{
    [RequireComponent(typeof(Toggle))]
    public sealed class BindableToggle : BaseBindable, IGettableBindable<bool>
    {
        private Toggle _component;

        private void Awake()
        {
            if (TryGetComponent(out _component))
                _component.onValueChanged.AddListener(OnValueChanged);
        }

        /// <inheritdoc />
        protected override void OnDisable()
        {
            base.OnDisable();
            if (_component != null)
                _component.onValueChanged.RemoveListener(OnValueChanged);
        }

        /// <inheritdoc />
        public void SetValue(bool value)
        {
            _component.SetIsOnWithoutNotify(value);
        }

        /// <inheritdoc />
        public bool GetValue()
        {
            return _component.isOn;
        }

        private void OnValueChanged(bool value) => 
            InvokeOnBindableChanged();
    }
}