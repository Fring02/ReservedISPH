using System;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;

namespace ISPH.Domain.Interfaces.Services
{
    public interface ICompaniesService : IEntityService<Company, Guid>
    {
        Task<Company> GetCompanyByNameAsync(string name);
    }
}