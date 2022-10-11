namespace CurrencyExchangeTrades.Service.TaskServices.Interfaces
{
    public interface IScopedProcessingService
    {
        Task SaveSymbols(CancellationToken stoppingToken);
    }
}
