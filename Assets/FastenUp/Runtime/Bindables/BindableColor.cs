using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// The one-way <see cref="IBindable"/> binds a value to the component with <see cref="Color"/> field.
    /// </summary>
    [RequireComponent(typeof(Graphic))]
    [AddComponentMenu(FastenUpComponentMenu.BaseMenu + "Bindable Color", 3)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Core-Functionalities#color")]
    public class BindableColor : BaseBindable, IBindable<Color>
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