using CanWeFixIt.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixIt.Api.Services.Interfaces
{
    public interface IMarketDataService
    {
        Task<IEnumerable<MarketDataDto>> GetMarketDataAsync(bool? active = null);
    }
}
