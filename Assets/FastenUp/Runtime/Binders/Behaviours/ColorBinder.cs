using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace FastenUp.Runtime.Binders.Behaviours
{
    /// <summary>
    /// The one-way <see cref="IBinder"/> binds a value to the component with <see cref="Color"/> field.
    /// </summary>
    [RequireComponent(typeof(Graphic))]
    [AddComponentMenu(FastenUpComponentMenu.BaseMenu + "Color Binder", 3)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Binders#color")]
    public class ColorBinder : BaseBinder, IValueReceiver<Color>
    {
        private Graphic _component;

        private void Awake()
        {
            _component = GetComponent<Graphic>();
        }

        /// <inheritdoc />
        public void SetValue(Color value)
        {
            _component.color = value;
        }
    }
}