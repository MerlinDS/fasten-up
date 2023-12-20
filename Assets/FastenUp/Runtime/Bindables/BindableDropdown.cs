using FastenUp.Runtime.Utils;
using TMPro;
using UnityEngine;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// The two-way <see cref="IBindable"/> binds a value to the component with <see cref="TMP_Dropdown"/> field.
    /// </summary>
    [RequireComponent(typeof(TMP_Dropdown))]
    [AddComponentMenu(FastenUpComponentMenu.BaseMenu + "Bindable Dropdown", 6)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Core-Functionalities#bindable-dropdown")]
    public sealed partial class BindableDropdown : BaseBindable, IGettableBindable<int>
    {
        private TMP_Dropdown _dropdown;
        
        private void Awake()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
            _dropdown.onValueChanged.AddListener(OnValueChanged);
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            _dropdown.onValueChanged.RemoveListener(OnValueChanged);
        }

        /// <inheritdoc />
        public void SetValue(int value)
        {
            _dropdown.SetValueWithoutNotify(value);
        }

        /// <inheritdoc />
        public int GetValue()
        {
            return _dropdown.value;
        }

        private void OnValueChanged(int _)
        {
            InvokeOnBindableChanged();
        }
    }
}