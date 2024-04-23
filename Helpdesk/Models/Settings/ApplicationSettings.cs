
namespace Helpdesk.Models.Settings
{
    public class ApplicationSettings : IApplicationSettings
    {
        public MemoryCacheOptions SessionMemoryCacheOptions { get; }

        public MemoryCacheEntryOptions SessionMemoryCacheEntryOptions { get; }

        public MemoryCacheOptions ScopeMemoryCacheOptions { get; }

        public MemoryCacheEntryOptions ScopeMemoryCacheEntryOptions { get; }

        public NavigationNode HomePage { get; }
        public uint MaxDynamicNavigationLength { get; }
        public bool AllowDynamicNavigationCycles { get; }
    

        public ApplicationSettings()
        {
            SessionMemoryCacheOptions = new MemoryCacheOptions()
            {

            };

            SessionMemoryCacheEntryOptions = new MemoryCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromMinutes(30),
            };

            ScopeMemoryCacheOptions = new MemoryCacheOptions()
            {

            };

            ScopeMemoryCacheEntryOptions = new MemoryCacheEntryOptions()
            {
                
            };
            HomePage = new NavigationNode(staticName: "Helpdesk", dynamicName:"Helpdesk", route: new NavigationRoute("", "Layout", "Index"), replaceWithSameControllerNodes: true);

            MaxDynamicNavigationLength = 8;

            AllowDynamicNavigationCycles = false;
        }
    }
}
