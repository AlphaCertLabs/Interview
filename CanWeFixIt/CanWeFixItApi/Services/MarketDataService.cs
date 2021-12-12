using CanWeFixIt.Api.Services.Interfaces;
using CanWeFixIt.Data.Models;
using CanWeFixIt.Data.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace CanWeFixIt.Api.Services
{
    public class MarketDataService : IMarketDataService
    {
        private readonly IDatabaseService _datatase;

        public MarketDataService(IDatabaseService database)
        {
            _datatase = database;
        }
        public async Task<IEnumerable<MarketDataDto>> GetMarketDataAsync(bool? active = null)
        {
            var marketDataDtoList = new List<MarketDataDto>();

            var marketDataList = await _datatase.GetMarketDataAsync(active);
            foreach (MarketData marketData in marketDataList)
            {
                var relatedInstrument = await _datatase.GetInstrumentsBySedolAsync(marketData.Sedol);

                if (relatedInstrument != null && relatedInstrument.Any())
                {
                    marketDataDtoList.Add(new MarketDataDto()
                    {
                        Id = marketData.Id,
                        DataValue = marketData.DataValue,
                        InstrumentId = relatedInstrument.First().Id,
                        Active = marketData.Active
                    });
                }
            }

            return marketDataDtoList;
        }
    }
}
