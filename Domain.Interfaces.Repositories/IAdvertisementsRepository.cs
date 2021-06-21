using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;

namespace ISPH.Domain.Interfaces.Repositories
{
    public interface IAdvertisementsRepository : IEntityRepository<Advertisement, Guid>
    {
        Task<IEnumerable<Advertisement>> GetAdvertisementsByCompanyAsync(Guid companyId);
        Task<IEnumerable<Advertisement>> GetAdvertisementsByEmployerAsync(Guid employerId);
        Task<IEnumerable<Advertisement>> GetAdvertisementsByPositionAsync(Guid positionId);
        Task<IEnumerable<Advertisement>> GetFilteredAdvertisementsAsync(Expression<Func<Advertisement, bool>> predicate);
        Task<int> GetAdvertisementsCountAsync();
        Task<IEnumerable<Advertisement>> GetAdvertisementsByPageAsync(int page);
        Task<uint> GetMaxAdvSalaryAsync();
        Task DeleteByEmployerAsync(Guid employerId);
    }
}
