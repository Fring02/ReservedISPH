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
    public class StudentRepository : EntityRepository<Student, Guid>, IStudentsRepository
    {
        public StudentRepository(EntityContext context) : base(context)
        {
        }
        public override async Task<bool> HasEntityAsync(Student entity)
        {
            return await _context.Students.AnyAsync(st => st.Email == entity.Email);
        }
        public override async Task<IEnumerable<Student>> GetAllAsync()
        {
           return await _context.Students.AsNoTracking().OrderBy(st => st.Id).
               Include(student => student.Resume).
               ToListAsync();
        }

        public async Task<Student> GetByEmailAsync(string email)
        {
            return await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Email == email);
        }

        public override async Task<Student> GetByIdAsync(Guid id)
        {
           return await _context.Students.AsNoTracking().
                Include(st => st.Resume).
                FirstOrDefaultAsync(st => st.Id == id);
        }

    }
}
