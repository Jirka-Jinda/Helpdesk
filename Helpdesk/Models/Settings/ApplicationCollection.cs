namespace Helpdesk.Models.Settings;

public static class ApplicationCollection
{
    public static IServiceCollection AddApplicationCollection(this IServiceCollection services, Action<ApplicationOptions> configureApplicationOptions)
    {
        ApplicationOptions options = new();
        configureApplicationOptions(options);

        services.AddSingleton(options);

        return services;
    }
}
