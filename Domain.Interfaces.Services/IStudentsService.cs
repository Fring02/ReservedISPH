using System;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Interfaces.Services.Auth;

namespace ISPH.Domain.Interfaces.Services
{
    public interface IStudentsService : IEntityService<Student, Guid>, IUserService<Student, Guid>
    {
    }
}