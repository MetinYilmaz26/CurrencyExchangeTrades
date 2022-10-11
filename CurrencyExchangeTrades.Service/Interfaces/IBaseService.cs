using CurrencyExchangeTrades.Domain.Entity;

namespace CurrencyExchangeTrades.Service.Interfaces
{
    public interface IBaseService<TEntity,TDto> where TEntity : BaseEntity where TDto : class
    {
        Task<int> Add(TDto dto);
        Task<int> AddRange(TDto[] dtos);
        Task<TDto> Get(int id);
        void Delete(int id);
        Task<List<TDto>> GetAll();
    }
}
