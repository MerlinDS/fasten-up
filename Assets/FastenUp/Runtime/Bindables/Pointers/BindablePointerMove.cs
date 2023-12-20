using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastenUp.Runtime.Bindables.Pointers
{
    /// <summary>
    /// This class is used to bind to the <see cref="IPointerUpHandler"/> interface.
    /// </summary>
    [AddComponentMenu(FastenUpComponentMenu.PointerMenu + nameof(BindablePointerMove), 0)]
    public sealed class BindablePointerMove : BindablePointer, IPointerMoveHandler
    {
        /// <inheritdoc />
        public void OnPointerMove(PointerEventData eventData)
        {
            OnPointerEvent(eventData);
        }
    }
}