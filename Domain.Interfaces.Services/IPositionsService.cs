using System;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Interfaces.Repositories;

namespace ISPH.Domain.Interfaces.Services
{
    public interface IPositionsService : IEntityService<IPositionsRepository, Position, Guid>
    {
        Task<Position> GetPositionByNameAsync(string name);
    }
}