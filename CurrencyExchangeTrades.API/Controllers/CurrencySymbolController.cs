using CurrencyExchangeTrades.Service.Dto;
using CurrencyExchangeTrades.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CurrencyExchangeTrades.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencySymbolController : ControllerBase
    {
        private readonly ICurrencySymbolService _currencySymbolService; 
        public CurrencySymbolController(ICurrencySymbolService currencySymbolService)
        {
            _currencySymbolService = currencySymbolService;
        }

        /// <summary>
        /// Get all currency symbols
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<CurrencySymbolDto>> GetAll()
        {
            Log.Information("Get all symbols.");
            return await _currencySymbolService.GetSymbols();
        }
    }
}
