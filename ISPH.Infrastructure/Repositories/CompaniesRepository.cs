using ISPH.Infrastructure.Data;
using ISPH.Core.Models;
using ISPH.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISPH.Core.DTO;
using System;

namespace ISPH.Infrastructure.Repositories
{
    public class CompaniesRepository : EntityRepository<Company, CompanyDto>, ICompanyRepository
    {
        public CompaniesRepository(EntityContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<CompanyDto>> GetAll()
        {
            return await Context.Companies.AsNoTracking().OrderBy(company => company.Name).Select(com => new CompanyDto(com)).
               ToListAsync();
        }
       
        public override async Task<bool> HasEntity(Company entity)
        {
            return await Context.Companies.AnyAsync(company => company.Name == entity.Name);
        }

        public async Task<Company> GetCompanyByName(string name)
        {
            return await Context.Companies.AsNoTracking().FirstOrDefaultAsync(company => company.Name == name);
        }

        public override async Task<Company> GetById(Guid id)
        {
           return await Context.Companies.AsNoTracking().FirstOrDefaultAsync(com => com.CompanyId == id);
        }
    }
}
