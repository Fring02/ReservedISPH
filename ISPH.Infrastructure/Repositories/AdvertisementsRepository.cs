using System;
using ISPH.Core.DTO;
using ISPH.Core.Enums;
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.Models;
using ISPH.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISPH.Core.DTO.Filter;

namespace ISPH.Infrastructure.Repositories
{
    public class AdvertisementsRepository : EntityRepository<Advertisement, AdvertisementDto>, IAdvertisementsRepository
    {
        private readonly IDictionary<EntityType, FilteredAds> _filteredMap;
        private delegate Task<IEnumerable<AdvertisementDto>> FilteredAds(Guid id);
        public AdvertisementsRepository(EntityContext context) : base(context)
        {
            _filteredMap = new Dictionary<EntityType, FilteredAds>
            {
                { EntityType.Company, GetAdvertisementsForCompany },
                { EntityType.Employer, GetAdvertisementsForEmployer },
                { EntityType.Position, GetAdvertisementsForPosition }
            };
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
                return await Delete(ad);
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
            return await Context.Advertisements.AsNoTracking().Include(adv => adv.Position).Include(adv => adv.Employer).
                ThenInclude(emp => emp.Company)
                .FirstOrDefaultAsync(adv => adv.AdvertisementId.Equals(id));
        }

        public async Task<IEnumerable<AdvertisementDto>> GetAdvertisementsByEntityId(Guid id, EntityType type)
        {
           return await _filteredMap[type].Invoke(id);
        }



        //Ads for specific model id
        private async Task<IEnumerable<AdvertisementDto>> GetAdvertisementsForEmployer(Guid employerid)
        {
            return await Context.Advertisements.AsNoTracking().
                Where(adv => adv.EmployerId.Equals(employerid)).OrderBy(adv => adv.Title).
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
                Where(adv => adv.PositionId.Equals(positionId)).OrderBy(adv => adv.Title).
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
                Where(adv => adv.Employer.CompanyId.Equals(companyId)).OrderBy(adv => adv.Title).
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
            var filtered = Context.Advertisements.OrderBy(adv => adv.Title).AsNoTracking();
            if (pos != Guid.Empty && com != Guid.Empty && sal > 0)
            {
                filtered = filtered.Where(adv => adv.Employer.CompanyId == com && adv.PositionId == pos &&
                adv.Salary >= sal - 25000 && adv.Salary <= sal + 25000);
            }
            else if (pos != Guid.Empty && com != Guid.Empty)
            {
                filtered = filtered.Where(adv => adv.Employer.CompanyId == com && adv.PositionId == pos);
            }
            else if (pos != Guid.Empty && sal > 0)
            {
                filtered = filtered.Where(adv => adv.PositionId == pos && adv.Salary >= sal - 25000 && adv.Salary <= sal + 25000);
            }
            else if (com != Guid.Empty && sal > 0)
            {
                filtered = filtered.Where(adv => adv.Employer.CompanyId == com && adv.Salary >= sal - 25000 &&
                adv.Salary <= sal + 25000);
            }
            else if (pos != Guid.Empty)
            {
                filtered = filtered.Where(adv => adv.PositionId == pos);
            }
            else if (com != Guid.Empty)
            {
                filtered = filtered.Where(adv => adv.Employer.CompanyId == com);
            }
            else if (sal > 0)
            {
                filtered = filtered.Where(adv => adv.Salary >= sal - 25000 && adv.Salary <= sal + 25000);
            }

            return await filtered.Select(adv => new AdvertisementDto()
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
