using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Dtos;
using ISPH.Domain.Interfaces.Repositories;
using ISPH.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ISPH.Infrastructure.Data.Repositories
{
    public class FeaturedAdvertisementsRepository : EntityRepository<FeaturedAdvertisement, Guid>, IFeaturedAdvertisementsRepository
    {
        public FeaturedAdvertisementsRepository(EntityContext context) : base(context)
        {
        }

        public async Task<FeaturedAdvertisement> GetByIdsAsync(Guid studentId, Guid adId)
        {
            return await _context.Favourites.AsNoTracking().
           FirstOrDefaultAsync(fav => fav.AdvertisementId == adId && fav.StudentId == studentId);
        }

        public async Task<IEnumerable<FeaturedAdvertisement>> GetFavouritesAsync(Guid studentId)
        {
            return await _context.Favourites.Where(fav => fav.StudentId == studentId).ToListAsync();
        }

        public override async Task<bool> HasEntityAsync(FeaturedAdvertisement entity)
        {
            return await _context.Favourites.AsNoTracking().
                AnyAsync(fav => fav.AdvertisementId == entity.AdvertisementId && fav.StudentId == entity.StudentId);
        }
    }
}
