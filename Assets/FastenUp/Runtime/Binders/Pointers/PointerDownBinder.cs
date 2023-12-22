using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastenUp.Runtime.Binders.Pointers
{
    /// <summary>
    /// This class is used to bind to the <see cref="IPointerDownHandler"/> interface.
    /// </summary>
    [AddComponentMenu(FastenUpComponentMenu.EventHandlersMenu + "Pointer Down Binder", 4)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Binders#pointer-down")]
    public sealed class PointerDownBinder : PointerBinder, IPointerDownHandler
    {
        /// <inheritdoc />
        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerEvent(eventData);
        }
    }
}