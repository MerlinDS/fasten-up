using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Mediators;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    internal partial class ExampleMediatorBindableSlider : MonoBehaviour, IMediator
    {
        private Bindable<Vector2Int> Slider { get; } = new();
        private Bindable<int> SliderValue { get; } = new();
        
        private Bindable<int> Text { get; } = new();
        
        private void Awake()
        {
            Slider.Value = new Vector2Int(0, 10);
            SliderValue.OnValueChanged += OnSliderValueChanged;
            SliderValue.Value = 0;
        }
        
        private void OnDestroy()
        {
            SliderValue.OnValueChanged -= OnSliderValueChanged;
        }

        private void OnSliderValueChanged(int value)
        {
            Text.Value = value;
        }
    }
}