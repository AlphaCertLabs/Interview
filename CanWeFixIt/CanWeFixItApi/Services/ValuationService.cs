using CanWeFixIt.Api.Services.Interfaces;
using CanWeFixIt.Data.Models;
using CanWeFixIt.Data.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace CanWeFixIt.Api.Services
{
    public class ValuationService : IValuationService
    {
        private readonly IDatabaseService _datatase;

        public ValuationService(IDatabaseService database)
        {
            _datatase = database;
        }

        public async Task<IEnumerable<MarketValuation>> GetMarketValuationsAsync(bool? active = null)
        {
            var marketDataList = await _datatase.GetMarketDataAsync(active);

            var marketValuationList = new List<MarketValuation>()
            {
                new MarketValuation()
                {
                    Total = marketDataList.Any() ? marketDataList.Sum(x => x.DataValue) : 0
                }
            };

            return marketValuationList;
        }
    }
}
