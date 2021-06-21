using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Dtos.Filter;
using ISPH.Domain.Interfaces.Repositories;

namespace ISPH.Domain.Interfaces.Services
{
    public interface IAdvertisementsService : 
        IEntityService<IAdvertisementsRepository, Advertisement, Guid>
    {
        
        Task<IEnumerable<Advertisement>> GetAdvertisementsByCompanyAsync(Guid companyId);
        Task<IEnumerable<Advertisement>> GetAdvertisementsByEmployerAsync(Guid employerId);
        Task<IEnumerable<Advertisement>> GetAdvertisementsByPositionAsync(Guid positionId);
        Task<IEnumerable<Advertisement>> GetFilteredAdvertisementsAsync(FilteredAdvertisementDto ad);
        Task<int> GetAdvertisementsCountAsync();
        Task<IEnumerable<Advertisement>> GetAdvertisementsByPageAsync(int page);
        Task<uint> GetMaxAdvSalaryAsync();
        Task DeleteByEmployerAsync(Guid employerId);
    }
}