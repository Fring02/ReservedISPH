using System;
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.Models;
using ISPH.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISPH.Core.DTO;

namespace ISPH.Infrastructure.Repositories
{
    public class FavouritesRepository : IFavouritesRepository
    {
        private readonly EntityContext _context;
        public FavouritesRepository(EntityContext context)
        {
            _context = context;
        }

        public async Task<FavouriteAdvertisement> GetById(Guid studentId, Guid adId)
        {
            return await _context.Favourites.AsNoTracking().
           FirstOrDefaultAsync(fav => fav.AdvertisementId == adId && fav.StudentId == studentId);
        }
        public async Task<bool> AddToFavourites(FavouriteAdvertisement ad)
        {
            await _context.Favourites.AddAsync(ad);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteFromFavourites(FavouriteAdvertisement ad)
        {
            _context.Favourites.Remove(ad);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<FavouriteAdvertisementDto>> GetFavourites(Guid studentId)
        {
            return await _context.Favourites.Where(fav => fav.StudentId == studentId).Select(fav =>
                new FavouriteAdvertisementDto()
                {
                    AdvertisementId = fav.AdvertisementId,
                    StudentId = fav.StudentId,
                    Title = fav.Advertisement.Title,
                }).ToListAsync();
        }
    }
}
