using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Mediators;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    internal partial class ExampleMediatorBindableColor : MonoBehaviour, IMediator
    {
        private Bindable<Color> Color { get; } = new() { Value = UnityEngine.Color.yellow };

        public void ChangeColorToRed()
        {
            ChangeColor(UnityEngine.Color.red);
        }

        public void ChangeColorToGreen()
        {
            ChangeColor(UnityEngine.Color.green);
        }

        public void ChangeColorToBlue()
        {
            ChangeColor(UnityEngine.Color.blue);
        }

        private void ChangeColor(Color color)
        {
            Color.Value = color;
        }
    }
}