using ISPH.Core.DTO;
using ISPH.Core.DTO.Filter;
using ISPH.Core.Enums;
using ISPH.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISPH.Core.Interfaces.Repositories
{
    public interface IAdvertisementsRepository : IEntityRepository<Advertisement, AdvertisementDto>
    {
        Task<IEnumerable<AdvertisementDto>> GetAdvertisementsByEntityId(Guid id, EntityType type);
        Task<IEnumerable<AdvertisementDto>> GetFilteredAdvertisements(string value);
        Task<IEnumerable<AdvertisementDto>> GetFilteredAdvertisements(FilteredAdvertisementDto ad);
        Task<int> GetAdvertisementsCount();

        Task<IEnumerable<AdvertisementDto>> GetAdvertisementsPerPage(int page);
        Task<IEnumerable<AdvertisementDto>> GetAdvertisementsAmount(int amount);

        Task<uint> GetMaxAdvSalary();

        Task<bool> UpdatePosition(Advertisement ad, Guid positionId);
    }
}
