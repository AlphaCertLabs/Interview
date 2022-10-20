namespace CanWeFixIt.Api.Data;

public static class DbInitializer
{
    public static void Initialize(CanWeFixItDbContext context)
    {
        if (context.Instruments.Any() || context.MarketData.Any())
        {
            return;   // assume the database has been seeded
        }

        var instruments = Seeder.GetInstruments();
        var marketData = Seeder.GetMarketData();

        context.Instruments.AddRange(instruments);
        context.MarketData.AddRange(marketData);

        context.SaveChanges();
    }
}
