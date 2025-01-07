
using Helpdesk.Models.EventPlanner.Manager;

namespace Helpdesk.Models.Settings
{
    /// <summary>
    /// All of application settings are drawn from here.
    /// If settings are not provided via json configuration file or source code, default implementation will be created and used.
    /// </summary>
    public interface IApplicationSettings
    {
        public MemoryCacheOptions SessionMemoryCacheOptions { get; }
        public MemoryCacheEntryOptions SessionMemoryCacheEntryOptions { get; }
        public MemoryCacheOptions ScopeMemoryCacheOptions { get; }
        public MemoryCacheEntryOptions ScopeMemoryCacheEntryOptions { get; }
        public NavigationTreeOptions NavigationTreeOptions { get; }
        public EventPlannerOptions EventPlannerOptions { get; }
        public TicketOptions TicketOptions { get; }
    }
}
