using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Mediators;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    internal partial class GraphicsOverviewMediator : MonoBehaviour, IMediator
    {
        public Bindable<bool> Visibility { get; } = new();
        private Bindable<Color> Color { get; } = new() { Value = UnityEngine.Color.white };

        private Bindable<Sprite> Sprite { get; } = new();
        private Bindable<string> SpritePath { get; } = new();

        private Bindable<bool> ImageVisibility { get; } = new() { Value = true };

        public void ChangeToRed()
        {
            Color.Value = new Color(1, 0.380503F, 0.380503F);
        }

        public void ChangeToBlue()
        {
            Color.Value = new Color(0.380503F, 0.380503F, 1);
        }

        public void ChangeToGreen()
        {
            Color.Value = new Color(0.380503F, 1, 0.380503F);
        }

        public void SetSprite(bool isOn)
        {
            Sprite.Value = isOn ? Resources.Load<Sprite>("FastenUp - Icon") : null;
        }

        public void SetSpritePath(bool isOn)
        {
            SpritePath.Value = isOn ? "FastenUp - Icon" : null;
        }

        public void ChangeImageVisibility(bool isOn)
        {
            ImageVisibility.Value = isOn;
        }
    }
}