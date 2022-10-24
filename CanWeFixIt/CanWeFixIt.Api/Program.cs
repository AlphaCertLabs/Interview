using CanWeFixIt.Api.Data;
using CanWeFixIt.Api.Models;
using CanWeFixIt.Api.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Net.Mime;

// configuration
var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

// if using Azure Application Insights, add connection string to config
//builder.Services.AddApplicationInsightsTelemetry();

var connectionString = configuration.GetConnectionString("CanWeFixItDbContext");

// in-memory is not designed to be used in production
// https://stackoverflow.com/questions/56319638/entityframeworkcore-sqlite-in-memory-db-tables-are-not-created
var keepAliveConnection = new SqliteConnection(connectionString);
keepAliveConnection.Open();

// add services to the container
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
        Description = "An ASP.NET Core Web API for the AlphaCert interview."
    });

    // use XML documentation for Swagger
    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = "swagger";
    });
}

// this web service runs on HTTP
//app.UseHttpsRedirection();

// these are not required for this API
//app.UseStaticFiles();
//app.UseCors();
//app.UseAuthorization();

app.CreateDbIfNotExists();

// routes
app.MapGet("/v1/instruments", async (ICanWeFixItRepository repository) =>
    (await repository.GetInstrumentsAsync()).ToArray())
    .Produces<Instrument[]>(StatusCodes.Status200OK, MediaTypeNames.Application.Json);

app.MapGet("/v1/marketdata", async (ICanWeFixItRepository repository) =>
    (await repository.GetMarketDataDtosAsync()).ToArray())
    .Produces<MarketDataDto[]>(StatusCodes.Status200OK, MediaTypeNames.Application.Json);

app.MapGet("/v1/valuations", async (ICanWeFixItRepository repository) =>
    (await repository.GetMarketValuationsAsync()).ToArray())
    .Produces<MarketValuation[]>(StatusCodes.Status200OK, MediaTypeNames.Application.Json);

app.Run();

// so that the integration tests have access
public partial class Program { }