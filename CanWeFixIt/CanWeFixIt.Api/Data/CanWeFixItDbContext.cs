using Microsoft.EntityFrameworkCore;
using CanWeFixItService.Models;

namespace CanWeFixIt.Api.Data
{
    public class CanWeFixItDbContext : DbContext
    {
        public CanWeFixItDbContext(DbContextOptions<CanWeFixItDbContext> options)
            : base(options)
        {
        }

        public DbSet<Instrument> Instruments => Set<Instrument>();
        public DbSet<MarketData> MarketData => Set<MarketData>();
    }
}
