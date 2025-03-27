using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, Action<DbOptions> configureDbOptions)
    {
        var options = new DbOptions();
        configureDbOptions(options);

        return services;
    }

    public static async Task<IServiceProvider> ApplyMigrations(this IServiceProvider serviceProvider, IConfigurationManager configuration)
    {
        bool.TryParse(configuration["DbSettings:UseInMemoryDatabase"], out bool useInMemoryDb);

        if (!useInMemoryDb)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<HelpdeskDbContext>();
            if (dbContext != null)
            {
                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    Console.WriteLine("Applying Migrations...");
                    await dbContext.Database.MigrateAsync();
                }
            }
        }

        return serviceProvider;
    }
}
