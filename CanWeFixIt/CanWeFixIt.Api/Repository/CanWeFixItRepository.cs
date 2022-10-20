using CanWeFixIt.Api.Data;
using CanWeFixItService.Models;
using Microsoft.EntityFrameworkCore;

namespace CanWeFixItService;

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
}