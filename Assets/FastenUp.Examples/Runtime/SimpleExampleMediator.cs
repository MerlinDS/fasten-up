using FastenUp.Runtime.Base;
using UnityEngine;
using UnityEngine.Events;

namespace FastenUp.Examples.Runtime
{
    public partial class SimpleExampleMediator : MonoBehaviour, IMediator
    {
        private BindPoint<string> Text { get; } = new();

        private BindPoint<Color> Color { get; } = new();
        
        private BindPoint<UnityAction> ButtonClick { get; } = new();
        private BindPoint<int> ColorChangeCount { get; } = new();

        private void Awake()
        {
            Text.Value = "Hello World!";
            Color.Value = UnityEngine.Color.yellow;
            ButtonClick.Value = ChangeColor;
        }

        private void ChangeColor()
        {
            Color.Value = Color.Value == UnityEngine.Color.yellow
                ? UnityEngine.Color.red
                : UnityEngine.Color.yellow;
            ColorChangeCount.Value++;
        }
    }
}