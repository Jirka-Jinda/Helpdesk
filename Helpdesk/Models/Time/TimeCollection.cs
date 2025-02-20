using Domain.Abstraction.ServiceInterfaces;

namespace Helpdesk.Models.Time;

public static class TimeCollection
{
    public static IServiceCollection AddTimeCollection(this IServiceCollection services, Action<TimeOptions> configureTimeOptions)
    {
        TimeOptions options = new TimeOptions();
        configureTimeOptions(options);

        services.AddSingleton(options);
        services.AddSingleton<ITimeService, TimeService>();

        return services;
    }
}
