using CanWeFixIt.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixIt.Api.Services.Interfaces
{
    public interface IValuationService
    {
        Task<IEnumerable<MarketValuation>> GetMarketValuationsAsync(bool? active = null);
    }
}
