using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// The one-way <see cref="IBindable"/> binds a value to the component with <see cref="UnityAction"/> field.
    /// </summary>
    [RequireComponent(typeof(Button))]
    [AddComponentMenu(FastenUpComponentMenu.BaseMenu + nameof(BindableButton) , 2)]
    public class BindableButton : BaseBindable, IBindable<UnityAction>
    {
        private Button _component;

        private void Awake()
        {
            _component = GetComponent<Button>();
        }

        /// <inheritdoc />
        public void SetValue(UnityAction value)
        {   
            if(value is null)
                return;
            
            _component.onClick.AddListener(value);
        }

        /// <inheritdoc />
        protected override void OnDisable()
        {
            base.OnDisable();
            _component.onClick.RemoveAllListeners();
        }
    }
}