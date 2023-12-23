using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastenUp.Runtime.Binders.Pointers
{
    /// <summary>
    /// This class is used to bind to the <see cref="IPointerUpHandler"/> interface.
    /// </summary>
    [RequireComponent(typeof(PointerDownBinder))]
    [AddComponentMenu(FastenUpComponentMenu.EventHandlersMenu + "Pointer Up Binder", 5)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Binders#pointerup")]
    public sealed class PointerUpBinder : PointerBinder, IPointerUpHandler
    {
        /// <inheritdoc />
        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerEvent(eventData);
        }
    }
}