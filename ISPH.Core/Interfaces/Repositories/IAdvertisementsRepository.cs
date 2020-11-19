using ISPH.Core.DTO;
using ISPH.Core.Enums;
using ISPH.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ISPH.Core.Interfaces.Repositories
{
    public interface IAdvertisementsRepository : IEntityRepository<Advertisement>
    {
        Task<IEnumerable<Advertisement>> GetAdvertisementsByEntityId(Guid id, EntityType type);
        Task<IEnumerable<Advertisement>> GetFilteredAdvertisements(string value);
        Task<IEnumerable<Advertisement>> GetFilteredAdvertisements(FilteredAdvertisementDto ad);
        Task<int> GetAdvertisementsCount();

        Task<IEnumerable<Advertisement>> GetAdvertisementsPerPage(int page);
        Task<IEnumerable<Advertisement>> GetAdvertisementsAmount(int amount);
    }
}
