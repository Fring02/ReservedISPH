using System;
using ISPH.Infrastructure.Data;
using ISPH.Core.Models;
using ISPH.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISPH.Infrastructure.Extensions;

namespace ISPH.Infrastructure.Repositories
{
    public class PositionsRepository : EntityRepository<Position>, IPositionsRepository
    {
        public PositionsRepository(EntityContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Position>> GetAll()
        {
           return await Context.Positions.AsNoTracking().JoinAdvertisements(Context).
               OrderBy(pos => pos.Name).ToListAsync();
        }

        public override async Task<Position> GetById(Guid id)
        {
            return await Context.Positions.AsNoTracking().Include(pos => pos.Advertisements).
                FirstOrDefaultAsync(pos => pos.PositionId == id);
        }

        public async Task<Position> GetPositionByName(string name)
        {
            return await Context.Positions.AsNoTracking().Include(pos => pos.Advertisements)
                .FirstOrDefaultAsync(pos => pos.Name == name);
        }

        public override async Task<bool> HasEntity(Position entity)
        {
            return await Context.Positions.AnyAsync(pos => pos.Name == entity.Name);
        }
        
    }
}
