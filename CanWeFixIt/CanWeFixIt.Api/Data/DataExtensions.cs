using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CanWeFixIt.Api.Data;

public static class DataExtensions
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<CanWeFixItDbContext>();

        context.CreateDbIfNotExists();
    }

    public static void CreateDbIfNotExists(this CanWeFixItDbContext context)
    {
        context.Database.EnsureCreated();
        DbInitializer.Initialize(context);
    }
}
