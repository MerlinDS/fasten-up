using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace FastenUp.Runtime.Binders.Behaviours
{
    /// <summary>
    /// The one-way <see cref="IBinder"/> binds a value to the component with <see cref="Sprite"/> field.
    /// </summary>
    [RequireComponent(typeof(Image))]
    [AddComponentMenu(FastenUpComponentMenu.BaseMenu + "Sprite Binder", 2)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Binders#sprite")]
    public sealed class SpriteBinder : BaseBinder, IValueReceiver<Sprite>
    {
        private Image _component;

        private void Awake()
        {
            _component = GetComponent<Image>();
        }

        public void SetValue(Sprite value)
        {
            _component.sprite = value;
            _component.enabled = value != null;
        }
    }
}