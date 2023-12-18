using FastenUp.Runtime.Base;
using UnityEngine;
using UnityEngine.Events;

namespace FastenUp.Examples.Runtime
{
    public partial class SimpleExampleMediator : MonoBehaviour, IMediator
    {
        private BindPoint<string> Text { get; } = new();

        private BindPoint<Color> Color { get; } = new();

        public BindPoint<bool> Toggle { get; } = new();

        public BindPoint<string> Image { get; } = new();

        private BindPoint<UnityAction> ButtonClick { get; } = new();
        private BindPoint<int> ColorChangeCount { get; } = new();


        private void Awake()
        {
            Color.OnValueChanged += OnColorValueChanged;
            Toggle.OnValueChanged += OnToggleValueChanged;

            Toggle.Value = true;
            Color.Value = UnityEngine.Color.yellow;
            ButtonClick.Value = ChangeColor;
            Image.Value = "FastenUp - Icon";
        }

        private void OnDestroy()
        {
            Color.OnValueChanged -= OnColorValueChanged;
            Toggle.OnValueChanged -= OnToggleValueChanged;
        }

        private void OnColorValueChanged(Color color)
        {
            ColorChangeCount.Value++;
        }

        private void OnToggleValueChanged(bool value)
        {
            Text.Value = value ? "Hello World!" : "Toggle is off!";
            Image.Value = value ? "FastenUp - Icon" : null;
        }

        private void ChangeColor()
        {
            Color.Value = Color.Value == UnityEngine.Color.yellow
                ? UnityEngine.Color.red
                : UnityEngine.Color.yellow;
        }
    }
}