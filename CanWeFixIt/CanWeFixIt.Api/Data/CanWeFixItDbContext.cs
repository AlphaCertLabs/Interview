using CanWeFixIt.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CanWeFixIt.Api.Data;

public class CanWeFixItDbContext : DbContext
{
    public CanWeFixItDbContext(DbContextOptions<CanWeFixItDbContext> options)
        : base(options)
    {
    }

    public DbSet<Instrument> Instruments => Set<Instrument>();
    public DbSet<MarketData> MarketData => Set<MarketData>();
}
