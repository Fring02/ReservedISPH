using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ISPH.Domain.Interfaces.Core;

namespace ISPH.Domain.Interfaces.Repositories
{
    public interface IEntityRepository<TEntity, in TId> where TEntity : class
    {
         Task<TEntity> CreateAsync(TEntity entity);
         Task UpdateAsync(TEntity entity);
         Task DeleteAsync(TEntity entity);
         Task<bool> HasEntityAsync(TEntity entity);
         Task<TEntity> GetByIdAsync(TId id);
         Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
