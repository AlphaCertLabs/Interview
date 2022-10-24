using CanWeFixIt.Api.Data;
using CanWeFixIt.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CanWeFixIt.Api.Repositories;

public class CanWeFixItRepository : ICanWeFixItRepository
{
    private readonly CanWeFixItDbContext _context;

    public CanWeFixItRepository(CanWeFixItDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Instrument>> GetInstrumentsAsync()
        => await _context.Instruments
            .Where(i => i.Active)
            .OrderBy(i => i.Id)
            .AsNoTracking()
            .ToListAsync();

    public async Task<IEnumerable<MarketData>> GetMarketDataAsync()
        => await _context.MarketData
            .Where(md => md.Active)
            .AsNoTracking()
            .ToListAsync();

    public async Task<IEnumerable<MarketDataDto>> GetMarketDataDtosAsync()
    {
        var instruments = await GetInstrumentsAsync();
        var marketData = await GetMarketDataAsync();

        // for more complex queries, this could be improved for performance
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
    }

    public async Task<IEnumerable<MarketValuation>> GetMarketValuationsAsync() =>
        new[]
        {
            new MarketValuation
            {
                Total = (await GetMarketDataAsync()).Sum(md => md.DataValue).GetValueOrDefault()
            }
        };
}