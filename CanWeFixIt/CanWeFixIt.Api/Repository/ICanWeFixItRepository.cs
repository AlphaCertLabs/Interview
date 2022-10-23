using CanWeFixItService.Models;

namespace CanWeFixItService;

public interface ICanWeFixItRepository
{
    Task<IEnumerable<Instrument>> GetInstrumentsAsync();
    Task<IEnumerable<MarketData>> GetMarketDataAsync();
    Task<IEnumerable<MarketDataDto>> GetMarketDataDtosAsync();
    Task<MarketValuation> GetMarketValuationAsync();
}