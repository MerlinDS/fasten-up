using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Mediators;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    internal partial class ExampleMediatorBindableToggle : MonoBehaviour, IMediator
    {
        private Bindable<bool> Toggle { get; } = new();
        private Bindable<string> Text { get; } = new();

        private void Awake()
        {
            Toggle.OnValueChanged += OnToggleValueChanged;
            Toggle.Value = true;
        }

        private void OnDestroy()
        {
            Toggle.OnValueChanged -= OnToggleValueChanged;
        }
        
        private void OnToggleValueChanged(bool value)
        {
            Text.Value = value ? "On" : "Off";
        }
    }
}