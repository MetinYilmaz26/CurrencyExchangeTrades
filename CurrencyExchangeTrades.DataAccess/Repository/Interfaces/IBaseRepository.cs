namespace CurrencyExchangeTrades.DataAccess.Repository.Interfaces
{
    public interface IBaseRepository<TEntity> 
    {
        Task<int> Add(TEntity entity);
        Task<int> AddRange(TEntity[] entities);
        void Delete(int id);
        Task<List<TEntity>> GetAll(string[]? navigationPropertyPaths = null);
        Task<TEntity> Get(int id, string[]? navigationPropertyPaths = null);

    }
}
