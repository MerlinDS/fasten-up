using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastenUp.Runtime.Bindables.Pointers
{
    /// <summary>
    /// This class is used to bind to the <see cref="IPointerExitHandler"/> interface.
    /// </summary> 
    [AddComponentMenu(FastenUpComponentMenu.EventHandlersMenu + "Pointer Exit Handler", 2)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Core-Functionalities#pointer-exit")]
    public sealed class BindablePointerExit : BindablePointer, IPointerExitHandler
    {
        /// <inheritdoc />
        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerEvent(eventData);
        }
    }
}