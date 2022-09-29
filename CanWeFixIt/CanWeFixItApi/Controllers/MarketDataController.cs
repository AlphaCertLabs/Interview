using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CanWeFixItService;
using Microsoft.AspNetCore.Mvc;

namespace CanWeFixItApi.Controllers
{
    [ApiController]
    [Route("v1/marketdata")]
    public class MarketDataController : ControllerBase
    {
        private readonly IDatabaseService _database;

        public MarketDataController(IDatabaseService database)
        {
            _database = database;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarketDataDto>>> GetMarketData()
        {
            try
            {
                return Ok(_database.MarketData().Result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}