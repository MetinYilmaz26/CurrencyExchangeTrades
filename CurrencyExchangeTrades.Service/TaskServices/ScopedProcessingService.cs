using CurrencyExchangeTrades.Service.Interfaces;
using CurrencyExchangeTrades.Service.TaskServices.Interfaces;
using Serilog;

namespace CurrencyExchangeTrades.Service.TaskServices
{

    public class ScopedProcessingService : IScopedProcessingService
    {
        private int executionCount = 0;
        private readonly ICurrencySymbolService _currencySymbolService;
        public ScopedProcessingService(ICurrencySymbolService currencySymbolService)
        {
            _currencySymbolService = currencySymbolService;
        }

        public async Task SaveSymbols(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;
                _currencySymbolService.SaveSymbols();
                Log.Information("Scoped Processing Service is working. Count: {Count}", executionCount);
                await Task.Delay((int)TimeSpan.FromDays(1).TotalMilliseconds, stoppingToken);
            }
        }
    }

}
