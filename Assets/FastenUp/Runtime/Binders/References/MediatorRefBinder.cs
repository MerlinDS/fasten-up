using FastenUp.Runtime.Mediators;
using FastenUp.Runtime.Utils;
using UnityEngine;

namespace FastenUp.Runtime.Binders.References
{
    /// <summary>
    /// Binds object references of type <see cref="IMediator"/> to a parent <see cref="IMediator"/>.
    /// </summary>
    [AddComponentMenu(FastenUpComponentMenu.References + "Mediator Reference Binder", 0)]
    // [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/...")]
    public sealed class MediatorRefBinder : BaseRefBinder<IMediator>
    {
        /// <inheritdoc />
        protected override bool IncludeOwnGameObjectInFind => false;
    }
}