using System;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Dtos;

namespace ISPH.Domain.Interfaces.Repositories
{
    public interface IEmployersRepository : IEntityRepository<Employer, Guid>
    {
        Task<Employer> GetByEmailAsync(string email);
    }
}
