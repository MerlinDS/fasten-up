using FastenUp.Runtime.Bindables.Pointers;
using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastenUp.Runtime.Binders.Pointers
{
    /// <summary>
    /// This class is used to bind to the <see cref="IPointerUpHandler"/> interface.
    /// </summary>
    [AddComponentMenu(FastenUpComponentMenu.EventHandlersMenu + "Pointer Move Binder", 3)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Binders#pointer-move")]
    public sealed class PointerMoveBinder : BindablePointer, IPointerMoveHandler
    {
        /// <inheritdoc />
        public void OnPointerMove(PointerEventData eventData)
        {
            OnPointerEvent(eventData);
        }
    }
}