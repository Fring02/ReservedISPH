using System.Collections.Generic;
using System.Threading.Tasks;
using ISPH.Domain.Interfaces.Repositories;
using ISPH.Domain.Interfaces.Services;

namespace ISPH.Infrastructure.Services.Services
{
    public class BaseService<TRepository, TEntity, TId> : IEntityService<TRepository, TEntity, TId>
        where TRepository : IEntityRepository<TEntity, TId> where TEntity : class
    {
        protected TRepository _repository;

        protected BaseService(TRepository repository)
        {
            _repository = repository;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public virtual async Task<bool> HasEntityAsync(TEntity entity)
        {
           return await _repository.HasEntityAsync(entity);
        }

        public virtual async Task<TEntity> GetByIdAsync(TId id)
        {
           return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}