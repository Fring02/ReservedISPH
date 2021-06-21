using System;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Dtos;

namespace ISPH.Domain.Interfaces.Repositories
{
    public interface IResumesRepository : IEntityRepository<Resume, Guid>
    {
    }
}
