using FastenUp.Runtime.Bindables.Pointers;
using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastenUp.Runtime.Binders.Pointers
{
    /// <summary>
    /// This class is used to bind to the <see cref="IPointerExitHandler"/> interface.
    /// </summary> 
    [AddComponentMenu(FastenUpComponentMenu.EventHandlersMenu + "Pointer Exit Binder", 2)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Binders#pointer-exit")]
    public sealed class PointerExitBinder : BindablePointer, IPointerExitHandler
    {
        /// <inheritdoc />
        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerEvent(eventData);
        }
    }
}