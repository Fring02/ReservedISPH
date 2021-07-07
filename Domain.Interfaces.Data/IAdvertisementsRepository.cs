using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;

namespace ISPH.Domain.Interfaces.Repositories
{
    public interface IAdvertisementsRepository : IEntityRepository<Advertisement, Guid>, IFilter<Advertisement, Guid>
    {
        Task<int> GetAdvertisementsCountAsync();
        Task<IEnumerable<Advertisement>> GetAdvertisementsByPageAsync(int page);
        Task<uint> GetMaxAdvSalaryAsync();
        Task DeleteByEmployerAsync(Guid employerId);
    }
}
