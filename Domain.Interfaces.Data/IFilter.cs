using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Interfaces.Core;

namespace ISPH.Domain.Interfaces.Repositories
{
    public interface IFilter<TEntity, TId> where TEntity : IEntity<TId>
    {
        Task<IReadOnlyCollection<TEntity>> GetBy(Expression<Func<TEntity, bool>> predicate);
    }
}