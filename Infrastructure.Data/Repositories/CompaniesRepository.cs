using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Interfaces.Repositories;
using ISPH.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ISPH.Infrastructure.Data.Repositories
{
    public class CompaniesRepository : EntityRepository<Company, Guid>, ICompaniesRepository
    {
        public CompaniesRepository(EntityContext context) : base(context)
        {
        }

        public override async Task<IReadOnlyCollection<Company>> GetAllAsync()
        {
            return await _context.Companies.AsNoTracking().OrderBy(company => company.Name).
               ToListAsync();
        }
       
        public override async Task<bool> HasEntityAsync(Company entity)
        {
            return await _context.Companies.AnyAsync(company => company.Name == entity.Name);
        }

        public async Task<Company> GetByNameAsync(string name)
        {
            return await _context.Companies.AsNoTracking().FirstOrDefaultAsync(company => company.Name == name);
        }

    }
}
