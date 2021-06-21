using System;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Interfaces.Repositories;

namespace ISPH.Domain.Interfaces.Services
{
    public interface IResumesService : IEntityService<IResumesRepository, Resume, Guid>
    {
        
    }
}