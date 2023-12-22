using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastenUp.Runtime.Binders.Pointers
{
    /// <summary>
    /// This class is used to bind to the <see cref="IPointerClickHandler"/> interface.
    /// </summary> 
    [AddComponentMenu(FastenUpComponentMenu.EventHandlersMenu + "Pointer Click Binder", 0)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Binders#pointer-click")]
    public sealed class PointerClickBinder : PointerBinder, IPointerClickHandler
    {
        /// <inheritdoc />
        public void OnPointerClick(PointerEventData eventData)
        {
            OnPointerEvent(eventData);
        }
    }
}