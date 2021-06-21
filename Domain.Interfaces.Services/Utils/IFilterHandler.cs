using System;
using System.Linq.Expressions;
using ISPH.Domain.Interfaces.Core;

namespace ISPH.Domain.Interfaces.Services.Utils
{
    public interface IFilterHandler<TEntity, TId> where TEntity : IEntity<TId>
    {
        IFilterHandler<TEntity, TId> With(Expression<Func<TEntity, bool>> predicate);
        Expression<Func<TEntity, bool>> Result { get; }
    }
}