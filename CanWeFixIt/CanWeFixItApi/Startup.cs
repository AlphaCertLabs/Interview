using CanWeFixIt.Api.Settings;
using CanWeFixIt.Api.Constants;
using CanWeFixIt.Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CanWeFixIt.Api.Services.Interfaces;
using CanWeFixIt.Api.Services;

namespace CanWeFixIt.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Insert AppConfig values
            var applicationSettingsConfigSection = _configuration.GetSection($"{Config.CANWEFIXIT_PREFIX}:{Config.APPLICATION_SETTINGS_SECTION}");
            services.Configure<ApplicationSettings>(applicationSettingsConfigSection);

            var appSettings = applicationSettingsConfigSection.Get<ApplicationSettings>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(appSettings.Version, new OpenApiInfo { Title = appSettings.Name, Version = appSettings.Version });
            });

            services.AddSingleton<IDatabaseService, DatabaseService>();
            services.AddTransient<IInstrumentService, InstrumentService>();
            services.AddTransient<IMarketDataService, MarketDataService>();
            services.AddTransient<IValuationService, ValuationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var appSettings = _configuration.GetSection($"{Config.CANWEFIXIT_PREFIX}:{Config.APPLICATION_SETTINGS_SECTION}").Get<ApplicationSettings>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/{appSettings.Version}/swagger.json", $"{appSettings.Name} {appSettings.Version}"));
            }

            // Populate in-memory database with data
            var database = app.ApplicationServices.GetService(typeof(IDatabaseService)) as IDatabaseService;
            database?.SetupDatabase();
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}