using FastenUp.Runtime.Base;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    internal partial class ExampleMediatorBindableInput : MonoBehaviour, IMediator
    {
        private BindPoint<string> InputText { get; } = new();
        private BindPoint<string> OutputText { get; } = new();

        private void Awake()
        {
            InputText.OnValueChanged += OnInputTextChanged;
            OnInputTextChanged(string.Empty);
        }
        
        private void OnDestroy()
        {
            InputText.OnValueChanged -= OnInputTextChanged;
        }

        private void OnInputTextChanged(string text)
        {
            OutputText.Value = $"You typed: {text}";
        }
    }
}