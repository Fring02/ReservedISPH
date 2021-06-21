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

        public override async Task<IEnumerable<Advertisement>> GetAllAsync()
        {
            return await _context.Advertisements.AsNoTracking().OrderBy(adv => adv.Title).
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
            return await _context.Advertisements.AsNoTracking().Skip((page - 1) * 4).Take(4).
                OrderBy(adv => adv.Title).
                ToListAsync();
        }

        public async Task<IEnumerable<Advertisement>> GetAdvertisementsByEmployerAsync(Guid employerId)
        {
            return await _context.Advertisements.AsNoTracking().
                Where(adv => adv.EmployerId == employerId).OrderBy(adv => adv.Title).ToListAsync();
        }

        public async Task<IEnumerable<Advertisement>> GetAdvertisementsByPositionAsync(Guid positionId)
        {
            return await _context.Advertisements.AsNoTracking().
                Where(adv => adv.PositionId.Equals(positionId)).OrderBy(adv => adv.Title).ToListAsync(); 
        }

        public async Task<IEnumerable<Advertisement>> GetAdvertisementsByCompanyAsync(Guid companyId)
        {
            return await _context.Advertisements.AsNoTracking().
                Where(adv => adv.Employer.CompanyId == companyId).OrderBy(adv => adv.Title).
                ToListAsync();
        }

        public async Task<IEnumerable<Advertisement>> GetFilteredAdvertisementsAsync(Expression<Func<Advertisement, bool>> predicate)
        {
            return await _context.Advertisements.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<int> GetAdvertisementsCountAsync()
        {
            return await _context.Advertisements.CountAsync();
        }

        /*public async Task<bool> UpdatePosition(Advertisement ad, Guid positionId)
        {
            ad.Position.Amount--;
            _context.Advertisements.Update(ad);
            ad.PositionId = positionId;
            var positionafter = await _context.Positions.FindAsync(positionId);
            positionafter.Amount++;
            return await _context.SaveChangesAsync() > 0;
        }*/
    }
}
