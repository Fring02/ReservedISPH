using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Dtos;
using ISPH.Domain.Interfaces.Repositories;

namespace ISPH.Domain.Interfaces.Services
{
    public interface IFeaturedAdvertisementsService : IEntityService<IFeaturedAdvertisementsRepository, FeaturedAdvertisement, Guid>
    {
        Task<FeaturedAdvertisement> GetByIdsAsync(Guid studentId, Guid adId);
        Task<IEnumerable<FeaturedAdvertisement>> GetFavouritesAsync(Guid id);
    }
}