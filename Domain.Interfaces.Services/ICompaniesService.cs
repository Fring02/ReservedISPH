using System;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Interfaces.Repositories;

namespace ISPH.Domain.Interfaces.Services
{
    public interface ICompaniesService : IEntityService<ICompaniesRepository, Company, Guid>
    {
        Task<Company> GetCompanyByNameAsync(string name);
    }
}