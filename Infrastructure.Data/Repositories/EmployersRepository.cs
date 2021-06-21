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
    public class EmployersRepository : EntityRepository<Employer, Guid>, IEmployersRepository
    {
        public EmployersRepository(EntityContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Employer>> GetAllAsync()
        {
           return await _context.Employers.AsNoTracking().OrderBy(emp => emp.Id).
                ToListAsync();
        }

        public async Task<Employer> GetByEmailAsync(string email)
        {
            return await _context.Employers.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email);
        }

        public override async Task<Employer> GetByIdAsync(Guid id)
        {
           return await _context.Employers.AsNoTracking().Include(emp => emp.Company).
                FirstOrDefaultAsync(adv => adv.Id == id);
        }
        public override async Task<bool> HasEntityAsync(Employer entity)
        {
            return await _context.Employers.AnyAsync(company => company.Email == entity.Email);
        }
    }
}
