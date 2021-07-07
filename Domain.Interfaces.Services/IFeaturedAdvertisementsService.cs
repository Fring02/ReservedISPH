using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Dtos;

namespace ISPH.Domain.Interfaces.Services
{
    public interface IFeaturedAdvertisementsService : IEntityService<FeaturedAdvertisement, Guid>
    {
        Task<FeaturedAdvertisement> GetByIdsAsync(Guid studentId, Guid adId);
        Task<IEnumerable<FeaturedAdvertisement>> GetFavouritesAsync(Guid id);
    }
}