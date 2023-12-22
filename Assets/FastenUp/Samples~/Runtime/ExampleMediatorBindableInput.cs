using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Mediators;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    internal partial class ExampleMediatorBindableInput : MonoBehaviour, IMediator
    {
        private Bindable<string> InputText { get; } = new();
        private Bindable<string> OutputText { get; } = new();

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