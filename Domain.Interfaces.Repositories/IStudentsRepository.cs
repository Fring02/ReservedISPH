using System;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Dtos;

namespace ISPH.Domain.Interfaces.Repositories
{
   public interface IStudentsRepository : IEntityRepository<Student, Guid>
   {
       Task<Student> GetByEmailAsync(string email);
   }
}
