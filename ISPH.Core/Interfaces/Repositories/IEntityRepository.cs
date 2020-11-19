﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISPH.Core.Interfaces.Repositories
{
    public interface IEntityRepository<T> where T : class
    {
         Task<bool> Create(T entity);
         Task<bool> Update(T entity);
         Task<bool> Delete(T entity);
         Task<bool> HasEntity(T entity);
         Task<T> GetById(Guid id);
         Task<IEnumerable<T>> GetAll();
    }
}
