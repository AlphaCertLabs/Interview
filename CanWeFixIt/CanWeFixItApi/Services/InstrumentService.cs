using CanWeFixIt.Api.Services.Interfaces;
using CanWeFixIt.Data.Models;
using CanWeFixIt.Data.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixIt.Api.Services
{
    public class InstrumentService : IInstrumentService
    {
        private readonly IDatabaseService _datatase;

        public InstrumentService(IDatabaseService database)
        {
            _datatase = database;
        }

        public async Task<IEnumerable<Instrument>> GetInstrumentsAsync(bool? active = null)
        {
            return await _datatase.GetInstrumentsAsync(active);
        }
    }
}
