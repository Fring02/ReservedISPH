using ISPH.Core.DTO;
using ISPH.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ISPH.Core.Interfaces.Repositories
{
    public interface IEmployersRepository : IEntityRepository<Employer, EmployerDto>
    {
        Task<bool> UpdatePassword(Employer employer, string password);
        Task<bool> UpdateCompany(Employer employer, string companyName);
    }
}
