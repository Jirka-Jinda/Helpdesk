using Helpdesk.Models.Storage.Manager;

namespace Helpdesk.Models.Storage;

public static class StorageCollection
{
    public static IServiceCollection AddStorageCollection(this IServiceCollection services, Action<StorageOptions> configureStorageOptions)
    {
        StorageOptions options = new StorageOptions();
        configureStorageOptions(options);

        services.AddSingleton(options);
        services.AddSingleton<ISessionStorage, SessionCache>();
        services.AddScoped<IScopeStorage, ScopeCache>();
        services.AddScoped<IStorageManager, StorageManager>();

        return services;
    }
}
