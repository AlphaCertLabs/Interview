using CanWeFixIt.Api.Data;
using CanWeFixIt.Api.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

// if using Azure Application Insights, add connection string to config
//builder.Services.AddApplicationInsightsTelemetry();

var connectionString = configuration.GetConnectionString("CanWeFixItDbContext");

// never do this in production
// https://stackoverflow.com/questions/56319638/entityframeworkcore-sqlite-in-memory-db-tables-are-not-created
var keepAliveConnection = new SqliteConnection(connectionString);
keepAliveConnection.Open();

builder.Services.AddSqlite<CanWeFixItDbContext>(connectionString);
builder.Services.AddScoped<ICanWeFixItRepository, CanWeFixItRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "CanWeFixIt API",
        Description = "An ASP.NET Core Web API for the AlphaCert interview.",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = "swagger";
    });
}

// this should be enabled for production
//app.UseHttpsRedirection();

app.UseStaticFiles();

//app.UseCors();

//app.UseAuthorization();

app.CreateDbIfNotExists();

// routes
app.MapGet("/v1/instruments", async (ICanWeFixItRepository repository) =>
    (await repository.GetInstrumentsAsync()).ToList());

app.MapGet("/v1/marketdata", async (ICanWeFixItRepository repository) =>
    (await repository.GetMarketDataDtosAsync()).ToList());

app.MapGet("/v1/valuations", async (ICanWeFixItRepository repository) =>
    await repository.GetMarketValuationsAsync());

//    .Produces<TodoItem>(StatusCodes.Status201Created, MediaTypeNames.Application.Json)
//    .Produces(StatusCodes.Status400BadRequest);

app.Run();

// so that the integration tests have access
public partial class Program { }