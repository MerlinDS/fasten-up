using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Mediators;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    internal partial class InteractiveOverviewMediator : MonoBehaviour, IMediator
    {
        public Bindable<bool> Visibility { get; } = new();
        private Bindable<bool> ToggleValue { get; } = new();

        private Bindable<string> ToggleState { get; } = new();
        
        private BindableSetup<Vector2> FloatSliderInit { get; } = new(new Vector2(-1, 1));
        
        private Bindable<float> FloatSlider { get; } = new();
        
        private Bindable<Vector2Int> IntSliderInit { get; } = new(new Vector2Int(-5, 5));
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