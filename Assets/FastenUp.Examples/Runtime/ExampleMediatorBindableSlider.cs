using FastenUp.Runtime.Base;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    internal partial class ExampleMediatorBindableSlider : MonoBehaviour, IMediator
    {
        private BindPoint<Vector2Int> Slider { get; } = new();
        private BindPoint<int> SliderValue { get; } = new();
        
        private BindPoint<int> Text { get; } = new();
        
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