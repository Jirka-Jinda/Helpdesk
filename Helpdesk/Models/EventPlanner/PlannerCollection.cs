
namespace Helpdesk.Models.EventPlanner;

public static class EventPlannerCollection
{
    public static IServiceCollection AddEventPlannerCollection(this IServiceCollection services, Action<EventPlannerOptions> configureStorageOptions)
    {
        EventPlannerOptions options = new EventPlannerOptions();
        configureStorageOptions(options);

        services.AddSingleton(options);

        services.AddSingleton<EventPlanner>();
        services.AddHostedService(ep => ep.GetRequiredService<EventPlanner>());
        services.AddTransient<IEventPlannerManager, EventPlannerManager>();

        return services;
    }
}
