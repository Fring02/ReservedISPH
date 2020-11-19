﻿using ISPH.Infrastructure.Data;
using ISPH.Core.Models;
using ISPH.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories
{
    public class CompaniesRepository : EntityRepository<Company>, ICompanyRepository
    {
        public CompaniesRepository(EntityContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Company>> GetAll()
        {
            return await Context.Companies.AsNoTracking().Select(com => new Company()
                {
                    CompanyId = com.CompanyId,
                    Name = com.Name
                }).OrderBy(company => company.Name).
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
    }
}
