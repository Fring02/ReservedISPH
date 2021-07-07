using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Dtos.Filter;
using ISPH.Domain.Interfaces.Repositories;
using ISPH.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ISPH.Infrastructure.Data.Repositories
{
    public class AdvertisementsRepository : EntityRepository<Advertisement, Guid>, IAdvertisementsRepository
    {
        public AdvertisementsRepository(EntityContext context) : base(context)
        {
        }
        public override async Task<bool> HasEntityAsync(Advertisement entity)
        {
            return await _context.Advertisements.AnyAsync(adv => adv.Title == entity.Title);
        }

        public override async Task<Advertisement> GetByIdAsync(Guid id)
        {
            return await _context.Advertisements.AsNoTracking().
                Include(adv => adv.Position).
                Include(adv => adv.Employer).
                ThenInclude(emp => emp.Company)
                .FirstOrDefaultAsync(adv => adv.Id == id);
        }

        public override async Task<IReadOnlyCollection<Advertisement>> GetAllAsync()
        {
            return await _context.Advertisements.AsNoTracking().OrderByDescending(adv => adv.Salary).
                ToListAsync(); 
        }

        public async Task<uint> GetMaxAdvSalaryAsync()
        {
            return await _context.Advertisements.MaxAsync(adv => adv.Salary);
        }

        public async Task DeleteByEmployerAsync(Guid employerId)
        {
            _context.Advertisements.RemoveRange(_context.Advertisements.
                Where(ad => ad.EmployerId == employerId));
            await Save();
        }

        public async Task<IEnumerable<Advertisement>> GetAdvertisementsByPageAsync(int page)
        {
            return await _context.Advertisements.AsNoTracking().Skip((page - 1) * 4).Take(4)
                .OrderByDescending(adv => adv.Salary).
                ToListAsync();
        }

        public async Task<IReadOnlyCollection<Advertisement>> GetBy(Expression<Func<Advertisement, bool>> predicate)
        {
            return await _context.Advertisements.AsNoTracking().
                Where(predicate).OrderByDescending(adv => adv.Salary).ToListAsync();
        }

        public async Task<int> GetAdvertisementsCountAsync()
        {
            return await _context.Advertisements.CountAsync();
        }
    }
}
