using FastenUp.Runtime.Utils;
using TMPro;
using UnityEngine;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// The two-way <see cref="IBindable"/> binds a value to the component with <see cref="TMP_InputField"/> field.
    /// </summary> 
    [RequireComponent(typeof(TMP_InputField))]
    [AddComponentMenu(FastenUpComponentMenu.BaseMenu + "Bindable Input Field" , 7)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Core-Functionalities#input-field")]
    public class BindableInputField : BaseBindable, IGettableBindable<string>
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
            InvokeOnBindableChanged();
    }
}