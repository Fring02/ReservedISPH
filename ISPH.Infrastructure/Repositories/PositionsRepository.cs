using System;
using ISPH.Infrastructure.Data;
using ISPH.Core.Models;
using ISPH.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISPH.Core.DTO;

namespace ISPH.Infrastructure.Repositories
{
    public class PositionsRepository : EntityRepository<Position, PositionDto>, IPositionsRepository
    {
        public PositionsRepository(EntityContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<PositionDto>> GetAll()
        {
           return await Context.Positions.AsNoTracking().OrderBy(pos => pos.Name).Select(pos => new PositionDto(pos)).
               ToListAsync();
        }

        public override async Task<Position> GetById(Guid id)
        {
            return await Context.Positions.AsNoTracking().Include(pos => pos.Advertisements).
                FirstOrDefaultAsync(pos => pos.PositionId == id);
        }

        public async Task<PositionDto> GetPositionByName(string name)
        {
            var p = await Context.Positions.AsNoTracking().Include(pos => pos.Advertisements)
                .FirstOrDefaultAsync(pos => pos.Name == name);
            return new PositionDto(p) { Advertisements = p.Advertisements };
        }

        public override async Task<bool> HasEntity(Position entity)
        {
            return await Context.Positions.AnyAsync(pos => pos.Name == entity.Name);
        }
        
    }
}
