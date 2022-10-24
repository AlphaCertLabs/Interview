using CanWeFixIt.Api.Models;

namespace CanWeFixIt.Api.Repositories;

public interface ICanWeFixItRepository
{
    Task<IEnumerable<Instrument>> GetInstrumentsAsync();
    Task<IEnumerable<MarketData>> GetMarketDataAsync();
    Task<IEnumerable<MarketDataDto>> GetMarketDataDtosAsync();
    Task<IEnumerable<MarketValuation>> GetMarketValuationsAsync();
}