using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISPH.Core.Interfaces.Repositories
{
    public interface IEntityRepository<T, D> where T : class where D : class 
    {
         Task<bool> Create(T entity);
         Task<bool> Update(T entity);
         Task<bool> Delete(T entity);
        Task<bool> DeleteById(Guid id);
         Task<bool> HasEntity(T entity);
         Task<T> GetById(Guid id);
         Task<IEnumerable<D>> GetAll();
    }
}
