using CurrencyExchangeTrades.Service.Dto;
using CurrencyExchangeTrades.Service.ExceptionHandler;
using CurrencyExchangeTrades.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CurrencyExchangeTrades.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;
        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }
        [HttpGet("{id}")]
        public async Task<TradeDto> Get(int id)
        {
           Log.Information("Get Trade By Id");
           return await _tradeService.Get(id);
        }

        /// <summary>
        /// Exchange money
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST 
        ///     {
        ///         "clientId": 1,
        ///         "tradeDate": "2022-10-09T13:01:31.814Z",
        ///         "from": "EUR",
        ///         "to": "USD",
        ///         "amount": 10,
        ///         "tradeType": "Buy"
        ///     }  
        ///  from and to symbol values get from api/CurrencySymbol - symbol value.  
        /// "tradeType": "Buy" or "Sell"
        /// </remarks>
        /// <param name="trade"> </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> SetTrade(TradeDto trade)
        {
           Log.Information("Set Trade By User :" + trade.ClientId);
            return await _tradeService.SetTrade(trade);
        }
        /// <summary>
        /// Exchange money
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST 
        ///     {
        ///         "clientId": 1,
        ///         "tradeDate": "2022-10-09T13:01:31.814Z",
        ///         "from": 1,
        ///         "to": 2,
        ///         "amount": 10,
        ///         "type": 0
        ///     }  
        ///  from and to Id values get from api/CurrencySymbol - id value.  
        /// "tradeType": Sell=0 Buy=1 (Default 0)
        /// </remarks>
        /// <param name="trade"> </param>
        /// <returns></returns>
        [HttpPost("withId")]
        public async Task<string> SetTrade(TradeInputDto trade)
        {
           Log.Information("Set Trade By User :" + trade.ClientId);
            return await _tradeService.SetTrade(trade);
        }

        /// <summary>
        /// Get all stored trades
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<TradeDto>> GetAll()
        {
            Log.Information("Get All Trades");
            return await _tradeService.GetAll();
        }

    }
}
