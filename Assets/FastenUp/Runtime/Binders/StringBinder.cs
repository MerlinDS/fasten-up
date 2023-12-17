using System;
using System.Globalization;
using FastenUp.Runtime.Base;
using FastenUp.Runtime.Bindables;
using TMPro;

namespace FastenUp.Runtime.Binders
{
    public sealed class StringBinder : BaseBindable, 
        IBindable<string>, IBindable<int>, IBindable<float>
    {
        private enum ComponentType
        {
            None,
            Text,
            InputField
        }

        private ComponentType _componentType;

        private TMP_InputField _inputField;
        private TMP_Text _textField;

        private void Start() 
        {
            if (_componentType != ComponentType.None)
                return;

            if (TryBindTextField())
            {
                _componentType = ComponentType.Text;
                return;
            }

            if (TryBindInputField())
            {
                _componentType = ComponentType.InputField;
                return;
            }

            throw new Exception("No suitable component found");
        }

        private bool TryBindTextField()
        {
            if (!TryGetComponent(out TMP_Text tmpText))
                return false;

            _textField = tmpText;
            return true;
        }

        private bool TryBindInputField()
        {
            if (!TryGetComponent(out TMP_InputField inputField))
                return false;

            inputField.onValueChanged.AddListener(OnValueChangedHandler);
            _inputField = inputField;
            return true;
        }

        private void OnDestroy()
        {
            if (_inputField != null)
                _inputField.onValueChanged.RemoveListener(OnValueChangedHandler);
            _componentType = ComponentType.None;
        }

        private void OnValueChangedHandler(string arg0) =>
            InvokeValueChanged<string>();

        public void SetValue(string value)
        {
            switch (_componentType)
            {
                case ComponentType.Text:
                    _textField.text = value;
                    break;
                case ComponentType.InputField:
                    _inputField.text = value;
                    break;
                case ComponentType.None:
                    throw new Exception("No suitable component found");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <inheritdoc />
        public void SetValue(int value) => 
            SetValue(value.ToString());

        /// <inheritdoc />
        public void SetValue(float value) => 
            SetValue(value.ToString(CultureInfo.InvariantCulture));

        /// <inheritdoc />
        public string GetValue()
        {
            return _componentType switch
            {
                ComponentType.Text => _textField.text,
                ComponentType.InputField => _inputField.text,
                ComponentType.None => throw new Exception("No suitable component found"),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        /// <inheritdoc />
        int IBindable<int>.GetValue() => 
            int.Parse(GetValue());

        /// <inheritdoc />
        float IBindable<float>.GetValue() => 
            float.Parse(GetValue(), CultureInfo.InvariantCulture);
    }
}