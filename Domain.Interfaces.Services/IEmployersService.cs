using System;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Interfaces.Repositories;
using ISPH.Domain.Interfaces.Services.Auth;

namespace ISPH.Domain.Interfaces.Services
{
    public interface IEmployersService : IEntityService<IEmployersRepository, Employer, Guid>, IUserService<Employer, Guid>
    {
        Task UpdateCompanyAsync(Employer employer, string companyName);
    }
}