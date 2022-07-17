using System.Collections.Generic;
using System.Threading.Tasks;
using CanWeFixItService.Models;
using CanWeFixItService.Models.DTOs;

namespace CanWeFixItService.Services.Interfaces
{
    public interface IDatabaseService
    {
        Task<IEnumerable<Instrument>> GetInstruments();
        Task<IEnumerable<MarketDataDto>> GetMarketData();
        Task<IEnumerable<MarketValuation>> GetValuations();
        void SetupDatabase();
    }
}