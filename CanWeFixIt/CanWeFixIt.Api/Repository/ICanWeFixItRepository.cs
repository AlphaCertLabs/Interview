using System.Collections.Generic;
using System.Threading.Tasks;
using CanWeFixItService.Models;

namespace CanWeFixItService;

public interface ICanWeFixItRepository
{
    Task<IEnumerable<Instrument>> GetInstrumentsAsync();
    Task<IEnumerable<MarketData>> GetMarketDataAsync();
}