using FastenUp.Runtime.Utils;
using TMPro;
using UnityEngine;

namespace FastenUp.Runtime.Binders.Behaviours
{
    /// <summary>
    /// The two-way <see cref="IBinder"/> binds a value to the component with <see cref="TMP_InputField"/> field.
    /// </summary> 
    [RequireComponent(typeof(TMP_InputField))]
    [AddComponentMenu(FastenUpComponentMenu.BaseMenu + "Input Field Binder" , 7)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Binders#input-field")]
    public class InputFieldBinder : BaseBinder, IValueReceiver<string>, IValueProvider<string>
    {
        private TMP_InputField _inputField;
        
        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();
            _inputField.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _inputField.onValueChanged.RemoveListener(OnValueChanged);
        }
        
        /// <inheritdoc />
        public void SetValue(string value)
        {
            _inputField.SetTextWithoutNotify(value);
        }

        /// <inheritdoc />
        public string GetValue()
        {
            return _inputField.text;
        }
        
        private void OnValueChanged(string _) => 
            InvokeOnBinderChanged();
    }
}