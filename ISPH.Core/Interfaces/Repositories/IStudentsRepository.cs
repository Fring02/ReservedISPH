﻿using ISPH.Core.Models;
using System.Threading.Tasks;
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.DTO;

namespace ISPH.Core.Interfaces.Repositories
{
   public interface IStudentsRepository : IEntityRepository<Student, StudentDto>
    {
        Task<bool> UpdatePassword(Student entity, string password);
    }
}
