namespace FastenUp.Runtime.Bindables
{
    public interface IHierarchyCache
    {
        /// <summary>
        /// Rebuilds hierarchy cache.
        /// <remarks>MUST be called after hierarchy was changed.</remarks>
        /// </summary>
        void RebuildCache();
    }
}