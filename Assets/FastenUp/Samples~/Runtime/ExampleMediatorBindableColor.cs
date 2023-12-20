using FastenUp.Runtime.Base;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    internal partial class ExampleMediatorBindableColor : MonoBehaviour, IMediator
    {
        private BindPoint<Color> Color { get; } = new() { Value = UnityEngine.Color.yellow };

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