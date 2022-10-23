using CanWeFixIt.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CanWeFixIt.Api.Tests
{
    internal class CanWeFixItWebApplicationFactory: WebApplicationFactory<global::Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.AddScoped(sp =>
                {
                    // Replace SQLite with the in memory provider for tests
                    return new DbContextOptionsBuilder<CanWeFixItDbContext>()
                                .UseInMemoryDatabase("Tests", root)
                                .UseApplicationServiceProvider(sp)
                                .Options;
                });
            });

            return base.CreateHost(builder);
        }
    }
}
