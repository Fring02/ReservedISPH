using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Interfaces.Repositories;
using ISPH.Domain.Interfaces.Services;
using ISPH.Domain.Models.Exceptions;

namespace ISPH.Infrastructure.Services.Services
{
    public class FeaturedAdvertisementsService : BaseService<IFeaturedAdvertisementsRepository, FeaturedAdvertisement, Guid>,
        IFeaturedAdvertisementsService
    {
        public FeaturedAdvertisementsService(IFeaturedAdvertisementsRepository repository) : base(repository)
        {
        }

        public override async Task<FeaturedAdvertisement> CreateAsync(FeaturedAdvertisement entity)
        {
            if (await HasEntityAsync(entity))
                throw new EntityPresentException("Such featured advertisement already exists");
            return await base.CreateAsync(entity);
        }

        public async Task<FeaturedAdvertisement> GetByIdsAsync(Guid studentId, Guid adId)
        {
            if (studentId == default || adId == default) return null;
            return await _repository.GetByIdsAsync(studentId, adId);
        }

        public async Task<IEnumerable<FeaturedAdvertisement>> GetFavouritesAsync(Guid id)
        {
            if (id == default) return null;
            return await _repository.GetFavouritesAsync(id);
        }
    }
}