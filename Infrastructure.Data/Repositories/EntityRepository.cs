using System.Collections.Generic;
using System.Threading.Tasks;
using ISPH.Domain.Interfaces.Repositories;
using ISPH.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ISPH.Infrastructure.Data.Repositories
{
   public abstract class EntityRepository<TEntity, TId> : IEntityRepository<TEntity, TId> where TEntity : class
   {
        protected readonly EntityContext _context;
        protected EntityRepository(EntityContext context)
        {
            _context = context;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await Save();
            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await Save();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await Save();
        }

        public abstract Task<bool> HasEntityAsync(TEntity entity);

        public virtual async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IReadOnlyCollection<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        protected async Task Save()
        {
            await _context.SaveChangesAsync();
        }
   }
}
