using System.Collections.Generic;
using System.Threading.Tasks;
using ISPH.Domain.Interfaces.Repositories;

namespace ISPH.Domain.Interfaces.Services
{
    public interface IEntityService<TRepository, TEntity, TId> where TEntity : class
    where TRepository : IEntityRepository<TEntity, TId>
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<bool> HasEntityAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(TId id);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}