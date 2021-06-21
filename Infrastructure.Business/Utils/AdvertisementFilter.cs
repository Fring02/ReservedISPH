using System;
using System.Linq;
using System.Linq.Expressions;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Interfaces.Services.Utils;

namespace ISPH.Infrastructure.Services.Utils
{
    public class AdvertisementFilter : IFilterHandler<Advertisement, Guid>
    {
        private Expression<Func<Advertisement, bool>> _predicate = a => true;
        public IFilterHandler<Advertisement, Guid> With(Expression<Func<Advertisement, bool>> predicate)
        {
            var invokedExpr = Expression.Invoke(predicate, Result.Parameters);
            _predicate = Expression.Lambda<Func<Advertisement, bool>>
                (Expression.AndAlso(Result.Body, invokedExpr), Result.Parameters);
            return this;
        }

        public Expression<Func<Advertisement, bool>> Result => _predicate;
    }
}