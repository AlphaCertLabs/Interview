using CanWeFixIt.Api.Data;
using CanWeFixIt.Api.Models;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http.Json;

namespace CanWeFixIt.Api.Tests;

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
    public void Initialize()
    {
        // all tests assume starting with the same seed data
        using var context = TestHelper.GetCanWeFixItDbContext();
        context.CreateDbIfNotExists();
    }

    [TestMethod]
    public async Task CanWeFixIt_GetInstruments_ReturnsOKWithActiveInstruments()
    {
        // arrange

        // act
        var response = await _httpClient.GetAsync("/v1/instruments");
        var actual = await response.Content.ReadFromJsonAsync<Instrument[]>();

        // assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        Assert.AreEqual(4, actual?.Length);

        Assert.AreEqual(actual?[0].Id, 2);
        Assert.AreEqual(actual?[0].Sedol, "Sedol2");
        Assert.AreEqual(actual?[0].Name, "Name2");
        Assert.IsTrue(actual?[0].Active);

        Assert.AreEqual(actual?[1].Id, 4);
        Assert.AreEqual(actual?[1].Sedol, "Sedol4");
        Assert.AreEqual(actual?[1].Name, "Name4");
        Assert.IsTrue(actual?[1].Active);

        Assert.AreEqual(actual?[2].Id, 6);
        Assert.AreEqual(actual?[2].Sedol, "");
        Assert.AreEqual(actual?[2].Name, "Name6");
        Assert.IsTrue(actual?[2].Active);

        Assert.AreEqual(actual?[3].Id, 8);
        Assert.AreEqual(actual?[3].Sedol, "Sedol8");
        Assert.AreEqual(actual?[3].Name, "Name8");
        Assert.IsTrue(actual?[3].Active);
    }

    [TestMethod]
    public async Task CanWeFixIt_GetMarketData_ReturnsOKWithCalculatedMarketData()
    {
        // arrange

        // act
        var response = await _httpClient.GetAsync("/v1/marketdata");
        var actual = await response.Content.ReadFromJsonAsync<MarketDataDto[]>();

        // assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        Assert.AreEqual(2, actual?.Length);

        Assert.AreEqual(actual?[0].Id, 2);
        Assert.AreEqual(actual?[0].DataValue, 2222);
        Assert.AreEqual(actual?[0].InstrumentId, 2);
        Assert.IsTrue(actual?[0].Active);

        Assert.AreEqual(actual?[1].Id, 4);
        Assert.AreEqual(actual?[1].DataValue, 4444);
        Assert.AreEqual(actual?[1].InstrumentId, 4);
        Assert.IsTrue(actual?[1].Active);
    }

    [TestMethod]
    public async Task CanWeFixIt_GetValuations_ReturnsOKWithCalculatedValuations()
    {
        // arrange

        // act
        var response = await _httpClient.GetAsync("/v1/valuations");
        var actual = await response.Content.ReadFromJsonAsync<MarketValuation[]>();

        // assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        Assert.AreEqual(1, actual?.Length);

        Assert.AreEqual("DataValueTotal", actual?[0].Name);
        Assert.AreEqual(13332, actual?[0].Total);
    }
}