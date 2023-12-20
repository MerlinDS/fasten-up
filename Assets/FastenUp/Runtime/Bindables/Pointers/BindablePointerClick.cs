using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastenUp.Runtime.Bindables.Pointers
{
    /// <summary>
    /// This class is used to bind to the <see cref="IPointerClickHandler"/> interface.
    /// </summary> 
    [AddComponentMenu(FastenUpComponentMenu.EventHandlersMenu + "Pointer Click Handler", 0)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Core-Functionalities#bindable-pointer-click")]
    public sealed class BindablePointerClick : BindablePointer, IPointerClickHandler
    {
        /// <inheritdoc />
        public void OnPointerClick(PointerEventData eventData)
        {
            OnPointerEvent(eventData);
        }
    }
}