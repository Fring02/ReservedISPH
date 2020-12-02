﻿using ISPH.Core.Models;
using System.Threading.Tasks;
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.DTO;

namespace ISPH.Core.Interfaces.Repositories
{
    public interface IPositionsRepository : IEntityRepository<Position, PositionDto>
    {
         Task<PositionDto> GetPositionByName(string name);
    }
    
}
