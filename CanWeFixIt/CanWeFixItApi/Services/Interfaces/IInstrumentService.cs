using CanWeFixIt.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixIt.Api.Services.Interfaces
{
    public interface IInstrumentService
    {
        Task<IEnumerable<Instrument>> GetInstrumentsAsync(bool? active = null);
    }
}
