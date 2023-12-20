using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastenUp.Runtime.Bindables.Pointers
{
    /// <summary>
    /// This class is used to bind to the <see cref="IPointerUpHandler"/> interface.
    /// </summary>
    [AddComponentMenu(FastenUpComponentMenu.PointerMenu + nameof(BindablePointerUp), 0)]
    public sealed class BindablePointerUp : BindablePointer, IPointerUpHandler
    {
        /// <inheritdoc />
        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerEvent(eventData);
        }
    }
}