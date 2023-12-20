using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastenUp.Runtime.Bindables.Pointers
{
    /// <summary>
    /// This class is used to bind to the <see cref="IPointerDownHandler"/> interface.
    /// </summary>
    [AddComponentMenu(FastenUpComponentMenu.PointerMenu + nameof(BindablePointerDown), 0)]
    public sealed class BindablePointerDown : BindablePointer, IPointerDownHandler
    {
        /// <inheritdoc />
        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerEvent(eventData);
        }
    }
}