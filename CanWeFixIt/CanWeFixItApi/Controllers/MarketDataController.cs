using System.Collections.Generic;
using System.Threading.Tasks;
using CanWeFixItService;
using Microsoft.AspNetCore.Mvc;

namespace CanWeFixItApi.Controllers
{
    [ApiController]
    [Route("v2/marketdata")]
    public class MarketDataController : ControllerBase
    {
        // GET
        public async Task<ActionResult<IEnumerable<MarketDataDto>>> Get()
        {
            // TODO:

            return NotFound();
        }
    }
}