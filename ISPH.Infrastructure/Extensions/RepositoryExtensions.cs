using System.Linq;
using ISPH.Core.DTO;
using ISPH.Core.Models;
using ISPH.Infrastructure.Data;

namespace ISPH.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static IQueryable<Position> JoinAdvertisements(this IQueryable<Position> pos, EntityContext context)
        {
            return pos.GroupJoin(context.Advertisements.Select(adv => new Advertisement
            {
                PositionId = adv.PositionId,
                AdvertisementId = adv.AdvertisementId,
                Description = adv.Description,
                Title = adv.Title,
                Salary = adv.Salary
            }), p => p.PositionId, a => a.PositionId, (p, a) => new Position()
            {
                PositionId = p.PositionId,
                Amount = p.Amount,
                Name = p.Name,
                Advertisements = a
            });
        }
    }
}