using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISPH.Domain.Interfaces.Services
{
    public interface IEntityService<TEntity, TId> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<bool> HasEntityAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(TId id);
        Task<IReadOnlyCollection<TEntity>> GetAllAsync();
    }
}