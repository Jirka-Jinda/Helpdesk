namespace Helpdesk.Models.Settings
{
    /// <summary>
    /// Implementation of IApplicationSettings, contains all required data to run application. Primarily uses 'appoptions.json' file.
    /// </summary>
    public class ApplicationSettings : IApplicationSettings
    {
        private const string path = "appoptions.json";

        public MemoryCacheOptions SessionMemoryCacheOptions { get; }

        public MemoryCacheEntryOptions SessionMemoryCacheEntryOptions { get; }

        public MemoryCacheOptions ScopeMemoryCacheOptions { get; }

        public MemoryCacheEntryOptions ScopeMemoryCacheEntryOptions { get; }

        public NavigationTreeOptions NavigationTreeOptions { get; }

        public EventPlannerOptions EventPlannerOptions { get; }

        public TicketOptions TicketOptions { get; }

        public ApplicationSettings(MemoryCacheOptions sessionMemoryCacheOptions, MemoryCacheEntryOptions sessionMemoryCacheEntryOptions, MemoryCacheOptions scopeMemoryCacheOptions, MemoryCacheEntryOptions scopeMemoryCacheEntryOptions, NavigationTreeOptions navigationTreeOptions, EventPlannerOptions eventPlannerOptions, TicketOptions ticketOptions)
        {
            SessionMemoryCacheOptions = sessionMemoryCacheOptions;
            SessionMemoryCacheEntryOptions = sessionMemoryCacheEntryOptions;
            ScopeMemoryCacheOptions = scopeMemoryCacheOptions;
            ScopeMemoryCacheEntryOptions = scopeMemoryCacheEntryOptions;
            NavigationTreeOptions = navigationTreeOptions;
            EventPlannerOptions = eventPlannerOptions;
            TicketOptions = ticketOptions;
        }

        public static ApplicationSettings GetApplicationSettings()
        {            
            ApplicationSettings? result;
            bool rewriteSettings = false; 
            string config = "";

            if (File.Exists(path))                        
                config = File.ReadAllText(path);
            if (config != null || config != string.Empty)
                try
                {
                    result = JsonSerializer.Deserialize<ApplicationSettings>(config!);
                }
                catch 
                {
                    result = CreateDefaultApplicationSettings();
                    rewriteSettings = true;
                }
            else
            {
                result = CreateDefaultApplicationSettings();
                rewriteSettings = true;
            }
            if (rewriteSettings)
            {
                var newConfigSerialized = JsonSerializer.Serialize(result);
                File.WriteAllTextAsync(path, newConfigSerialized);
            }
            return result!;
        }

        public static ApplicationSettings CreateDefaultApplicationSettings()
        {
            return new ApplicationSettings
                (
                    // Session
                    new MemoryCacheOptions
                    {

                    },
                    new MemoryCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromMinutes(30)
                    },
                    // Scope
                    new MemoryCacheOptions
                    {

                    },
                    new MemoryCacheEntryOptions
                    {

                    },
                    // Navigation
                    new NavigationTreeOptions
                    {
                        HomePage = new NavigationRoute("", "Layout", "Index"),
                        StaticNodeLevel = 1,
                        AllowCyclicTree = false
                    },
                    // Event Planner
                    new EventPlannerOptions
                    {
                        CleanupTimerInterval = TimeSpan.FromMinutes(1),                        
                    },
                    new TicketOptions
                    {

                    }
                );
        }
    }
}
