namespace Helpdesk.Models.Settings
{
    public interface IApplicationSettings
    {
        public MemoryCacheOptions SessionMemoryCacheOptions { get; }
        public MemoryCacheEntryOptions SessionMemoryCacheEntryOptions { get; }
        public MemoryCacheOptions ScopeMemoryCacheOptions { get; }
        public MemoryCacheEntryOptions ScopeMemoryCacheEntryOptions { get; }
        public NavigationNode HomePage { get; }
        uint MaxDynamicNavigationLength { get; }
        bool AllowDynamicNavigationCycles { get; }
    }
}
