using CanWeFixIt.Api.Data;
using CanWeFixIt.Api.Tests;
using CanWeFixItService.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace CanWeFixIt.Tests;

[TestClass]
public class IntegrationTests
{
    private readonly HttpClient _httpClient;

    public IntegrationTests()
    {
        var webAppFactory = new CanWeFixItWebApplicationFactory();
        _httpClient = webAppFactory.CreateDefaultClient();
    }

    [TestInitialize]
    public async Task Initialize()
    {
        using var context = TestHelper.GetCanWeFixItDbContext();
        context.CreateDbIfNotExists();
        // TODO: convert to in-memory
        // all tests assume an empty database
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Instruments");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM MarketData");
    }

    [TestMethod]
    public async Task CanWeFixIt_GetInstruments_ReturnsOK()
    {
        // arrange
        var instruments = new Instrument[]
        {
            new Instrument { Id = 1, Sedol = "Sedol1", Name = "Name1", Active = false },
            new Instrument { Id = 2, Sedol = "Sedol2", Name = "Name2", Active = true },
            new Instrument { Id = 3, Sedol = "Sedol3", Name = "Name3", Active = false },
            new Instrument { Id = 4, Sedol = "Sedol4", Name = "Name4", Active = true },
            new Instrument { Id = 5, Sedol = "Sedol5", Name = "Name5", Active = false },
            new Instrument { Id = 6, Sedol = "", Name = "Name6", Active = true },
            new Instrument { Id = 7, Sedol = "Sedol7", Name = "Name7", Active = false },
            new Instrument { Id = 8, Sedol = "Sedol8", Name = "Name8", Active = true },
            new Instrument { Id = 9, Sedol = "Sedol9", Name = "Name9", Active = false },
        };

        using var context = TestHelper.GetCanWeFixItDbContext();
        await context.AddRangeAsync(instruments);
        _ = await context.SaveChangesAsync();

        // act
        var actual = (await _httpClient.GetFromJsonAsync<Instrument[]>($"/v1/instruments"))
            ?.ToArray();

        // assert
        Assert.AreEqual(actual?[0].Id, 2);
        Assert.AreEqual(actual?[0].Sedol, "Sedol2");
        Assert.AreEqual(actual?[0].Name, "Name2");
        Assert.IsTrue(actual?[0].Active);
    }
}