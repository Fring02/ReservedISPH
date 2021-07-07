using System;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;

namespace ISPH.Domain.Interfaces.Services
{
    public interface IPositionsService : IEntityService<Position, Guid>
    {
        Task<Position> GetPositionByNameAsync(string name);
    }
}