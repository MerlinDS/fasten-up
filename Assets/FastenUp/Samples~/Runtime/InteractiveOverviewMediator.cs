using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Mediators;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    internal partial class InteractiveOverviewMediator : MonoBehaviour, IMediator
    {
        private Bindable<bool> ToggleValue { get; } = new();

        private Bindable<string> ToggleState { get; } = new();
        
        private Bindable<Vector2> FloatSliderInit { get; } = new() { Value = new Vector2(-1, 1) };
        
        private Bindable<float> FloatSlider { get; } = new();
        
        private Bindable<Vector2Int> IntSliderInit { get; } = new() { Value = new Vector2Int(-5, 5) };
        private Bindable<int> IntSlider { get; } = new();

        private void Awake()
        {
            ToggleValue.OnValueChanged += OnToggleValueChanged;
            ToggleValue.Value = true;
        }

        private void OnDestroy()
        {
            ToggleValue.OnValueChanged -= OnToggleValueChanged;
        }
        
        private void OnToggleValueChanged(bool value)
        {
            ToggleState.Value = value ? "On" : "Off";
        }
    }
}