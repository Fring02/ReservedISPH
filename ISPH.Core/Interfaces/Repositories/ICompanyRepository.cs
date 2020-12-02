using ISPH.Core.DTO;
using ISPH.Core.Models;
using System.Threading.Tasks;

namespace ISPH.Core.Interfaces.Repositories
{
    public interface ICompanyRepository : IEntityRepository<Company, CompanyDto>
    {
         Task<Company> GetCompanyByName(string name);
    }
}
