using AutoMapper;
using CurrencyExchangeTrades.DataAccess.Repository.Interfaces;
using CurrencyExchangeTrades.Domain.Entity;
using CurrencyExchangeTrades.Service.Interfaces;

namespace CurrencyExchangeTrades.Service.Services
{
    public class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto> where TEntity : BaseEntity where TDto : class
    {
        private readonly IBaseRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        public BaseService(IBaseRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Add(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            return await _repository.Add(entity);
        }

        public async Task<int> AddRange(TDto[] dtos)
        {
            return await _repository.AddRange(_mapper.Map<TEntity[]>(dtos));
        }

        public async void Delete(int id)
        {
           _repository.Delete(id);
        }

        public async Task<TDto> Get(int id)
        {
            return _mapper.Map<TDto>(await _repository.Get(id));
        }

        public async Task<List<TDto>> GetAll()
        {
            return _mapper.Map<List<TDto>>(await _repository.GetAll());
        }
    }
}
