using System;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Interfaces.Repositories;
using ISPH.Domain.Interfaces.Services;
using ISPH.Domain.Models.Exceptions;

namespace ISPH.Infrastructure.Services.Services
{
    public class CompaniesService : BaseService<ICompaniesRepository, Company, Guid>, ICompaniesService
    {
        public CompaniesService(ICompaniesRepository repository) : base(repository)
        {
        }

        public override async Task<Company> CreateAsync(Company entity)
        {
            if (await HasEntityAsync(entity))
                throw new EntityPresentException("Company with name " + entity.Name + " already exists");
            return await base.CreateAsync(entity);
        }

        public async Task<Company> GetCompanyByNameAsync(string name)
        {
            if (!string.IsNullOrEmpty(name)) return null;
            return await _repository.GetByNameAsync(name);
        }
    }
}