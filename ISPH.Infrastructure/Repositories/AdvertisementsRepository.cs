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

namespace ISPH.Infrastructure.Repositories
{
    public class AdvertisementsRepository : EntityRepository<Advertisement>, IAdvertisementsRepository
    {
        private readonly IDictionary<EntityType, FilteredAds> _filteredMap;
        private delegate Task<IEnumerable<Advertisement>> FilteredAds(Guid id);
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

        public override async Task<IEnumerable<Advertisement>> GetAll()
        {
            return await Context.Advertisements.AsNoTracking().
                Include(adv => adv.Employer).
                ToListAsync(); 
        }

        public async Task<IEnumerable<Advertisement>> GetAdvertisementsAmount(int amount)
        {
            return await Context.Advertisements.AsNoTracking().Take(amount).OrderBy(adv => adv.Title).
                Include(adv => adv.Employer).
                ToListAsync();
        }

        public async Task<IEnumerable<Advertisement>> GetAdvertisementsPerPage(int page)
        {
            return await Context.Advertisements.AsNoTracking().Skip((page - 1) * 4).Take(4).
                OrderBy(adv => adv.Title).
                Include(adv => adv.Employer).
                ToListAsync();
        }

        public override async Task<Advertisement> GetById(Guid id)
        {
            return await Context.Advertisements.AsNoTracking().
                Include(adv => adv.Employer).
                FirstOrDefaultAsync(adv => adv.AdvertisementId == id);
        }

        public async Task<IEnumerable<Advertisement>> GetAdvertisementsByEntityId(Guid id, EntityType type)
        {
           return await _filteredMap[type].Invoke(id);
        }



        //Ads for specific model id
        private async Task<IEnumerable<Advertisement>> GetAdvertisementsForEmployer(Guid employerid)
        {
            return await Context.Advertisements.AsNoTracking().
                Where(adv => adv.EmployerId == employerid)
                .Include(adv => adv.Employer).ToListAsync();
        }

        private async Task<IEnumerable<Advertisement>> GetAdvertisementsForPosition(Guid positionId)
        {
            return await Context.Advertisements.AsNoTracking().
                Where(adv => adv.PositionId == positionId)
                .Include(adv => adv.Employer).ToListAsync(); 
        }

        private async Task<IEnumerable<Advertisement>> GetAdvertisementsForCompany(Guid companyId)
        {
            return await Context.Advertisements.AsNoTracking().
                Where(adv => adv.Employer.CompanyId == companyId).
                Include(adv => adv.Employer).
                ToListAsync();
        }


        //filtering
        public async Task<IEnumerable<Advertisement>> GetFilteredAdvertisements(string value)
        {
            var sql = string.Format("SELECT a.\"AdvertisementId\", e.\"EmployerId\", a.\"Title\", a.\"PositionId\"," +
                " a.\"Salary\", a.\"Description\", a.\"PositionName\", e.\"CompanyId\", e.\"CompanyName\"" +
               " FROM \"Advertisements\" a INNER JOIN \"Employers\" e ON a.\"EmployerId\" = e.\"EmployerId\" WHERE " +
                "a.\"PositionName\" LIKE '%{0}%' OR e.\"CompanyName\" LIKE '%{0}%' ORDER BY a.\"Title\"", value);
            return await Context.Advertisements.FromSqlRaw(sql).AsNoTracking().
                Include(adv => adv.Employer).ToListAsync();
        }

        public async Task<IEnumerable<Advertisement>> GetFilteredAdvertisements(FilteredAdvertisementDto ad)
        {
            string query = FilteredQuery(ad);
            return await Context.Advertisements.FromSqlRaw(query).AsNoTracking().
                Include(adv => adv.Employer).
                OrderBy(adv => adv.Title).ToListAsync();
        }

        public async Task<int> GetAdvertisementsCount()
        {
            return await Context.Advertisements.CountAsync();
        }


        private string FilteredQuery(FilteredAdvertisementDto ad)
        {
            var builder = new StringBuilder();
            var sql = "SELECT a.\"AdvertisementId\", e.\"EmployerId\", a.\"Title\", a.\"PositionId\"," +
                    " a.\"Salary\", a.\"Description\", a.\"PositionName\", e.\"CompanyId\", e.\"CompanyName\" " +
                   " FROM \"Advertisements\" a INNER JOIN \"Employers\" e ON a.\"EmployerId\" = e.\"EmployerId\" ";
            builder.Append(sql);
            if (ad.AnyValue(ad.CompanyName, ad.PositionName, ad.Salary))
            {
                builder.Append("WHERE ");
            }   
            string pos = ad.PositionName, com = ad.CompanyName;
            int sal = ad.Salary.GetValueOrDefault();
            bool posExists = !string.IsNullOrEmpty(pos), comExists = !string.IsNullOrEmpty(com), salExists = sal > 0;
            if(posExists && comExists && salExists)
            {
                builder.Append(string.Format("a.\"PositionName\" LIKE '%{0}%' AND e.\"CompanyName\" LIKE '%{1}%' AND a.\"Salary\" = {2}", pos, com, sal));
            }
            else if(posExists && comExists)
            {
                builder.Append(string.Format("a.\"PositionName\" LIKE '%{0}%' AND e.\"CompanyName\" LIKE '%{1}%'", pos, com));
            }
            else if(posExists && salExists)
            {
                builder.Append(string.Format("a.\"PositionName\" LIKE '%{0}%' AND a.\"Salary\" = {1}", pos, sal));
            }
            else if(comExists && salExists)
            {
                builder.Append(string.Format("e.\"CompanyName\" LIKE '%{0}%' AND a.\"Salary\" = {1}", com, sal));
            }
            else if (posExists)
            {
                builder.Append(string.Format("a.\"PositionName\" LIKE '%{0}%'", pos));
            }
            else if (comExists)
            {
                builder.Append(string.Format("e.\"CompanyName\" LIKE '%{0}%'", com));
            }
            else if (salExists)
            {
                builder.Append(string.Format("a.\"Salary\" = {0}", sal));
            }

            return builder.ToString();
        }
    }
}
