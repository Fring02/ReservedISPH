using System;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;

namespace ISPH.Domain.Interfaces.Repositories
{
    public interface IPositionsRepository : IEntityRepository<Position, Guid>
    {
         Task<Position> GetByNameAsync(string name);
    }
    
}
