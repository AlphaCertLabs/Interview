using CanWeFixIt.Api.Data;
using CanWeFixItService;
using CanWeFixItService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

// if using Azure Application Insights, add connection string to config
//builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddSqlite<CanWeFixItDbContext>(configuration.GetConnectionString("CanWeFixItDbContext"));
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

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
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

app.UseHttpsRedirection();

app.UseStaticFiles();

//app.UseCors();

//app.UseAuthorization();

app.CreateDbIfNotExists();

//routes
app.MapGet("/v1/instruments", async (ICanWeFixItRepository repository) =>
    (await repository.GetInstrumentsAsync()).ToList());

app.MapGet("/v1/marketdata", async (ICanWeFixItRepository repository) =>
{
    var instruments = (await repository.GetInstrumentsAsync());
    var marketData = (await repository.GetMarketDataAsync());

    // mapping is done here since DTO is a "view-model" and doesn't belong in the repository
    // for more complex queries, it could be improved for performance
    // for more complex mappings, use AutoMapper
    var dto = marketData
        .Where(md => instruments.Any(i => i.Sedol == md.Sedol))
        .Select(md => new MarketDataDto
        {
            Id = md.Id,
            DataValue = md.DataValue,
            InstrumentId = instruments.Single(i => i.Sedol == md.Sedol).Id,
            Active = md.Active
        })
        .ToList();

    return dto;
});

//    .Produces<TodoItem>(StatusCodes.Status201Created, MediaTypeNames.Application.Json)
//    .Produces(StatusCodes.Status400BadRequest);

app.Run();

// so that the integration tests have access
public partial class Program { }