using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CanWeFixItService.Models;
using CanWeFixItService.Models.DTOs;
using CanWeFixItService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CanWeFixItApi.Controllers
{
    [ApiController]
    [Route("v1")]
    public class V1Controller : Controller
    {
        private readonly IDatabaseService _database;

        public V1Controller(IDatabaseService database)
        {
            _database = database;
        }

        [HttpGet("instruments")]
        public async Task<ActionResult<IEnumerable<Instrument>>> GetInstruments()
        {
            var res = await _database.GetInstruments();
            return Ok(res);
        }

        [HttpGet("marketdata")]
        public async Task<ActionResult<IEnumerable<MarketDataDto>>> GetMarketData()
        {
            try
            {
                var res = await _database.GetMarketData();
                return Ok(res);
            }
            catch (Exception e)
            {
                //Log error

                //return 500 with exception message
                return BadRequest($"Unable to get marketdata {e.Message}");
            }
        }

        [HttpGet("valuations")]
        public async Task<ActionResult<IEnumerable<MarketValuation>>> GetValuations()
        {
            var res = await _database.GetValuations();
            return Ok(res);
        }
    }
}

