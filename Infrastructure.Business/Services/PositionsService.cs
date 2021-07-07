using System;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Interfaces.Repositories;
using ISPH.Domain.Interfaces.Services;
using ISPH.Domain.Models.Exceptions;

namespace ISPH.Infrastructure.Services.Services
{
    public class PositionsService : BaseService<IPositionsRepository, Position, Guid>, IPositionsService
    {
        public PositionsService(IPositionsRepository repository) : base(repository)
        {
        }

        public override async Task<Position> CreateAsync(Position entity)
        {
            if (await HasEntityAsync(entity))
                throw new EntityPresentException("Position with name " + entity.Name + " already exists");
            return await base.CreateAsync(entity);
        }

        public async Task<Position> GetPositionByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            return await _repository.GetByNameAsync(name);
        }
    }
}