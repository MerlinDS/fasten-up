using FastenUp.Runtime.Utils;
using TMPro;
using UnityEngine;

namespace FastenUp.Runtime.Binders.Behaviours
{
    /// <summary>
    /// The two-way <see cref="IBinder"/> binds a value to the component with <see cref="TMP_Dropdown"/> field.
    /// </summary>
    [RequireComponent(typeof(TMP_Dropdown))]
    [AddComponentMenu(FastenUpComponentMenu.BaseMenu + "Dropdown Binder", 6)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Binders#dropdown")]
    public sealed partial class DropdownBinder : BaseBinder, IValueReceiver<int>, IValueProvider<int>
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
            InvokeOnBinderChanged();
        }
    }
}