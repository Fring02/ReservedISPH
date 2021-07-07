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
    public class PositionsRepository : EntityRepository<Position, Guid>, IPositionsRepository
    {
        public PositionsRepository(EntityContext context) : base(context)
        {
        }

        public override async Task<IReadOnlyCollection<Position>> GetAllAsync()
        {
           return await _context.Positions.AsNoTracking().OrderBy(pos => pos.Name).
               ToListAsync();
        }

        public async Task<Position> GetByNameAsync(string name)
        {
           return await _context.Positions.FirstOrDefaultAsync(pos => pos.Name == name);
        }

        public override async Task<bool> HasEntityAsync(Position entity)
        {
            return await _context.Positions.AnyAsync(pos => pos.Name == entity.Name);
        }
        
    }
}
