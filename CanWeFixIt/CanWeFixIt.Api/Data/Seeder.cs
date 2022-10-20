using CanWeFixItService.Models;

namespace CanWeFixIt.Api.Data
{
    public static class Seeder
    {
        internal static Instrument[] GetInstruments()
            => new Instrument[]
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

        internal static MarketData[] GetMarketData()
            => new MarketData[]
            {
                new MarketData { Id = 1, DataValue = 1111, Sedol = "Sedol1", Active = false },
                new MarketData { Id = 2, DataValue = 2222, Sedol = "Sedol2", Active = true },
                new MarketData { Id = 3, DataValue = 3333, Sedol = "Sedol3", Active = false },
                new MarketData { Id = 4, DataValue = 4444, Sedol = "Sedol4", Active = true },
                new MarketData { Id = 5, DataValue = 5555, Sedol = "Sedol5", Active = false },
                new MarketData { Id = 6, DataValue = 6666, Sedol = "Sedol6", Active = true },
            };
    }
}
