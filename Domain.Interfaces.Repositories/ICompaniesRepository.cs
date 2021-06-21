using System;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;

namespace ISPH.Domain.Interfaces.Repositories
{
    public interface ICompaniesRepository : IEntityRepository<Company, Guid>
    {
         Task<Company> GetCompanyByNameAsync(string name);
    }
}
