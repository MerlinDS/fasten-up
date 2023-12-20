using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastenUp.Runtime.Bindables.Pointers
{
    /// <summary>
    /// This class is used to bind to the <see cref="IPointerEnterHandler"/> interface.
    /// </summary> 
    [AddComponentMenu(FastenUpComponentMenu.EventHandlersMenu + "Pointer Enter Handler", 1)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Core-Functionalities#pointer-enter")]
    public sealed class BindablePointerEnter : BindablePointer, IPointerEnterHandler
    {
        /// <inheritdoc />
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEvent(eventData);
        }
    }
}