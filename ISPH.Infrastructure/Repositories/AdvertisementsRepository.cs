using System;
using ISPH.Core.DTO;
using ISPH.Core.Enums;
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.Models;
using ISPH.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISPH.Infrastructure.Extensions;
using ISPH.Core.DTO.Filter;

namespace ISPH.Infrastructure.Repositories
{
    public class AdvertisementsRepository : EntityRepository<Advertisement, AdvertisementDto>, IAdvertisementsRepository
    {
        private readonly IDictionary<EntityType, FilteredAds> _filteredMap;
        private delegate Task<IEnumerable<AdvertisementDto>> FilteredAds(Guid id);
        public AdvertisementsRepository(EntityContext context) : base(context)
        {
            _filteredMap = new Dictionary<EntityType, FilteredAds>();
            if (!_filteredMap.ContainsKey(EntityType.Company)) _filteredMap.Add(EntityType.Company, GetAdvertisementsForCompany);
            if (!_filteredMap.ContainsKey(EntityType.Employer)) _filteredMap.Add(EntityType.Employer, GetAdvertisementsForEmployer);
            if (!_filteredMap.ContainsKey(EntityType.Position)) _filteredMap.Add(EntityType.Position, GetAdvertisementsForPosition);
        }
        public override async Task<bool> Create(Advertisement entity)
        {
            await Context.Advertisements.AddAsync(entity);
            var position = await Context.Positions.FirstOrDefaultAsync(pos => pos.PositionId == entity.PositionId);
            position.Amount++;
            return await Context.SaveChangesAsync() > 0;
        }
        public override async Task<bool> HasEntity(Advertisement entity)
        {
            return await Context.Advertisements.AnyAsync(adv => adv.Title == entity.Title);
        }
        public override async Task<bool> Delete(Advertisement entity)
        {
            Context.Advertisements.Remove(entity);
            var position = await Context.Positions.FirstOrDefaultAsync(pos => pos.PositionId == entity.PositionId);
            position.Amount--;
            return await Context.SaveChangesAsync() > 0;
        }
        public override async Task<bool> DeleteById(Guid id)
        {
            var ad = await Context.Advertisements.FindAsync(id);
            if(ad != null)
            {
                Context.Advertisements.Remove(ad);
                var pos = await Context.Positions.FirstOrDefaultAsync(p => p.PositionId == ad.PositionId);
                pos.Amount--;
                return await Context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public override async Task<IEnumerable<AdvertisementDto>> GetAll()
        {
            return await Context.Advertisements.AsNoTracking().OrderBy(adv => adv.Title).
                Select(adv => new AdvertisementDto()
                {
                    AdvertisementId = adv.AdvertisementId,
                    Title = adv.Title,
                    Salary = adv.Salary,
                    PositionName = adv.Position.Name,
                    CompanyId = adv.Employer.CompanyId,
                    Employer = new EmployerDto
                    {
                        CompanyName = adv.Employer.Company.Name
                    }
                }).
                ToListAsync(); 
        }


        public async Task<uint> GetMaxAdvSalary()
        {
            return await Context.Advertisements.MaxAsync(adv => adv.Salary);
        }

        public async Task<IEnumerable<AdvertisementDto>> GetAdvertisementsAmount(int amount)
        {
            return await Context.Advertisements.AsNoTracking().Take(amount).OrderBy(adv => adv.Title).
                Select(adv => new AdvertisementDto()
                {
                    AdvertisementId = adv.AdvertisementId,
                    Title = adv.Title,
                    Salary = adv.Salary,
                    PositionName = adv.Position.Name,
                    CompanyId = adv.Employer.CompanyId,
                    Employer = new EmployerDto
                    {
                        CompanyName = adv.Employer.Company.Name
                    }
                }).
                ToListAsync();
        }

        public async Task<IEnumerable<AdvertisementDto>> GetAdvertisementsPerPage(int page)
        {
            return await Context.Advertisements.AsNoTracking().Skip((page - 1) * 4).Take(4).
                OrderBy(adv => adv.Title).
                Select(adv => new AdvertisementDto()
                {
                    AdvertisementId = adv.AdvertisementId,
                    Title = adv.Title,
                    Salary = adv.Salary,
                    PositionName = adv.Position.Name,
                    CompanyId = adv.Employer.CompanyId,
                    Employer = new EmployerDto
                    {
                        FirstName = adv.Employer.FirstName,
                        LastName = adv.Employer.LastName,
                        Email = adv.Employer.Email,
                        CompanyName = adv.Employer.Company.Name
                    }
                }).
                ToListAsync();
        }

        public override async Task<Advertisement> GetById(Guid id)
        {
            return await Context.Advertisements.AsNoTracking().Include(adv => adv.Position).Include(adv => adv.Employer).ThenInclude(emp => emp.Company)
                .FirstOrDefaultAsync(adv => adv.AdvertisementId == id);
        }

        public async Task<IEnumerable<AdvertisementDto>> GetAdvertisementsByEntityId(Guid id, EntityType type)
        {
           return await _filteredMap[type].Invoke(id);
        }



        //Ads for specific model id
        private async Task<IEnumerable<AdvertisementDto>> GetAdvertisementsForEmployer(Guid employerid)
        {
            return await Context.Advertisements.AsNoTracking().
                Where(adv => adv.EmployerId == employerid).OrderBy(adv => adv.Title).
                Select(adv => new AdvertisementDto()
                {
                    AdvertisementId = adv.AdvertisementId,
                    Title = adv.Title,
                    Salary = adv.Salary,
                    PositionName = adv.Position.Name,
                    CompanyId = adv.Employer.CompanyId,
                    Employer = new EmployerDto
                    {
                        CompanyName = adv.Employer.Company.Name
                    }
                }).ToListAsync();
        }

        private async Task<IEnumerable<AdvertisementDto>> GetAdvertisementsForPosition(Guid positionId)
        {
            return await Context.Advertisements.AsNoTracking().
                Where(adv => adv.PositionId == positionId).OrderBy(adv => adv.Title).
                Select(adv => new AdvertisementDto()
                {
                    AdvertisementId = adv.AdvertisementId,
                    Title = adv.Title,
                    Salary = adv.Salary,
                    PositionName = adv.Position.Name,
                    CompanyId = adv.Employer.CompanyId,
                    Employer = new EmployerDto
                    {
                        FirstName = adv.Employer.FirstName,
                        LastName = adv.Employer.LastName,
                        Email = adv.Employer.Email,
                        CompanyName = adv.Employer.Company.Name
                    }
                }).ToListAsync(); 
        }

        private async Task<IEnumerable<AdvertisementDto>> GetAdvertisementsForCompany(Guid companyId)
        {
            return await Context.Advertisements.AsNoTracking().
                Where(adv => adv.Employer.CompanyId == companyId).OrderBy(adv => adv.Title).
                Select(adv => new AdvertisementDto()
                {
                    AdvertisementId = adv.AdvertisementId,
                    Title = adv.Title,
                    Salary = adv.Salary,
                    PositionName = adv.Position.Name,
                    CompanyId = adv.Employer.CompanyId,
                    Employer = new EmployerDto
                    {
                        CompanyName = adv.Employer.Company.Name
                    }
                }).
                ToListAsync();
        }


        //filtering
        public async Task<IEnumerable<AdvertisementDto>> GetFilteredAdvertisements(string value)
        {
            return await Context.Advertisements.Where(adv => adv.Position.Name.Contains(value) ||
            adv.Employer.Company.Name.Contains(value)).AsNoTracking().OrderBy(adv => adv.Title).
                Select(adv => new AdvertisementDto()
                {
                    AdvertisementId = adv.AdvertisementId,
                    Title = adv.Title,
                    Salary = adv.Salary,
                    PositionName = adv.Position.Name,
                    Employer = new EmployerDto
                    {
                        CompanyName = adv.Employer.Company.Name
                    }
                }).ToListAsync();
        }

        public async Task<IEnumerable<AdvertisementDto>> GetFilteredAdvertisements(FilteredAdvertisementDto ad)
        {
            Guid pos = ad.PositionId, com = ad.CompanyId;
            uint sal = ad.Salary.GetValueOrDefault();
            if (pos != Guid.Empty && com != Guid.Empty && sal > 0)
            {
                return await Context.Advertisements.Where(adv => adv.Employer.CompanyId == com && adv.PositionId == pos && adv.Salary >= sal - 25000 && adv.Salary <= sal + 25000).
                    AsNoTracking().OrderBy(adv => adv.Title).
                Select(adv => new AdvertisementDto()
                {
                    AdvertisementId = adv.AdvertisementId,
                    Title = adv.Title,
                    Salary = adv.Salary,
                    PositionName = adv.Position.Name,
                    Employer = new EmployerDto
                    {
                        CompanyName = adv.Employer.Company.Name
                    }
                }).ToListAsync();
            }
            else if (pos != Guid.Empty && com != Guid.Empty)
            {
                return await Context.Advertisements.Where(adv => adv.Employer.CompanyId == com && adv.PositionId == pos).
                    AsNoTracking().OrderBy(adv => adv.Title).
                Select(adv => new AdvertisementDto()
                {
                    AdvertisementId = adv.AdvertisementId,
                    Title = adv.Title,
                    Salary = adv.Salary,
                    PositionName = adv.Position.Name,
                    Employer = new EmployerDto
                    {
                        CompanyName = adv.Employer.Company.Name
                    }
                }).ToListAsync();
            }
            else if (pos != Guid.Empty && sal > 0)
            {
                return await Context.Advertisements.Where(adv => adv.PositionId == pos && adv.Salary >= sal - 25000 && adv.Salary <= sal + 25000).
                    AsNoTracking().OrderBy(adv => adv.Title).
                Select(adv => new AdvertisementDto()
                {
                    AdvertisementId = adv.AdvertisementId,
                    Title = adv.Title,
                    Salary = adv.Salary,
                    PositionName = adv.Position.Name,
                    Employer = new EmployerDto
                    {
                        CompanyName = adv.Employer.Company.Name
                    }
                }).ToListAsync();
            }
            else if (com != Guid.Empty && sal > 0)
            {
                return await Context.Advertisements.Where(adv => adv.Employer.CompanyId == com && adv.Salary >= sal - 25000 && adv.Salary <= sal + 25000).
                    AsNoTracking().OrderBy(adv => adv.Title).
                Select(adv => new AdvertisementDto()
                {
                    AdvertisementId = adv.AdvertisementId,
                    Title = adv.Title,
                    Salary = adv.Salary,
                    PositionName = adv.Position.Name,
                    Employer = new EmployerDto
                    {
                        CompanyName = adv.Employer.Company.Name
                    }
                }).ToListAsync();
            }
            else if (pos != Guid.Empty)
            {
                return await Context.Advertisements.Where(adv => adv.PositionId == pos).
                    AsNoTracking().OrderBy(adv => adv.Title).
                Select(adv => new AdvertisementDto()
                {
                    AdvertisementId = adv.AdvertisementId,
                    Title = adv.Title,
                    Salary = adv.Salary,
                    PositionName = adv.Position.Name,
                    Employer = new EmployerDto
                    {
                        CompanyName = adv.Employer.Company.Name
                    }
                }).ToListAsync();
            }
            else if (com != Guid.Empty)
            {
                return await Context.Advertisements.Where(adv => adv.Employer.CompanyId == com).
                     AsNoTracking().OrderBy(adv => adv.Title).
                 Select(adv => new AdvertisementDto()
                 {
                     AdvertisementId = adv.AdvertisementId,
                     Title = adv.Title,
                     Salary = adv.Salary,
                     PositionName = adv.Position.Name,
                     Employer = new EmployerDto
                     {
                         CompanyName = adv.Employer.Company.Name
                     }
                 }).ToListAsync();
            }
            else if (sal > 0)
            {
                return await Context.Advertisements.Where(adv => adv.Salary >= sal - 25000 && adv.Salary <= sal + 25000).
                    AsNoTracking().OrderBy(adv => adv.Title).
                Select(adv => new AdvertisementDto()
                {
                    AdvertisementId = adv.AdvertisementId,
                    Title = adv.Title,
                    Salary = adv.Salary,
                    PositionName = adv.Position.Name,
                    Employer = new EmployerDto
                    {
                        CompanyName = adv.Employer.Company.Name
                    }
                }).ToListAsync();
            }

            return await Context.Advertisements.AsNoTracking().OrderBy(adv => adv.Title).
                Select(adv => new AdvertisementDto()
                {
                    AdvertisementId = adv.AdvertisementId,
                    Title = adv.Title,
                    Salary = adv.Salary,
                    PositionName = adv.Position.Name,
                    Employer = new EmployerDto
                    {
                        CompanyName = adv.Employer.Company.Name
                    }
                }).ToListAsync();
        }

        public async Task<int> GetAdvertisementsCount()
        {
            return await Context.Advertisements.CountAsync();
        }

        public async Task<bool> UpdatePosition(Advertisement ad, Guid positionId)
        {
            ad.Position.Amount--;
            Context.Advertisements.Update(ad);
            ad.PositionId = positionId;
            var positionafter = await Context.Positions.FindAsync(positionId);
            positionafter.Amount++;
            return await Context.SaveChangesAsync() > 0;
        }
    }
}
