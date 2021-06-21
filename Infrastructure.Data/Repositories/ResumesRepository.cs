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
    public class ResumesRepository : EntityRepository<Resume, Guid>, IResumesRepository
    {
        public ResumesRepository(EntityContext context) : base(context)
        {
        }
        public override async Task<Resume> GetByIdAsync(Guid id)
        {
           return await _context.Resumes.AsNoTracking().Include(res => res.Student).FirstOrDefaultAsync(res => res.StudentId == id);
        }

        public override async Task<bool> HasEntityAsync(Resume resume)
        {
            return await _context.Resumes.AnyAsync(res => res.StudentId == resume.StudentId);
        }
    }
}
