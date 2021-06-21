using System;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Interfaces.Repositories;
using ISPH.Domain.Interfaces.Services;
using ISPH.Domain.Models.Exceptions;

namespace ISPH.Infrastructure.Services.Services
{
    public class ResumesService : BaseService<IResumesRepository, Resume, Guid>, IResumesService
    {
        public ResumesService(IResumesRepository repository) : base(repository)
        {
        }

        public override async Task<Resume> CreateAsync(Resume entity)
        {
            if (await HasEntityAsync(entity)) throw new EntityPresentException("Resume already exists");
            return await base.CreateAsync(entity);
        }
    }
}