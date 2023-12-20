using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastenUp.Runtime.Bindables.Pointers
{
    /// <summary>
    /// This class is used to bind to the <see cref="IPointerUpHandler"/> interface.
    /// </summary>
    [AddComponentMenu(FastenUpComponentMenu.EventHandlersMenu + "Pointer Move Handler", 3)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Core-Functionalities#pointer-move")]
    public sealed class BindablePointerMove : BindablePointer, IPointerMoveHandler
    {
        /// <inheritdoc />
        public void OnPointerMove(PointerEventData eventData)
        {
            OnPointerEvent(eventData);
        }
    }
}