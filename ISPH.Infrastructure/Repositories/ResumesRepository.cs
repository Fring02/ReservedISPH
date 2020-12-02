using System;
using ISPH.Infrastructure.Data;
using ISPH.Core.Models;
using ISPH.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ISPH.Core.DTO;

namespace ISPH.Infrastructure.Repositories
{
    public class ResumesRepository : EntityRepository<Resume, ResumeDto>, IResumesRepository
    {
        public ResumesRepository(EntityContext context) : base(context)
        {
        }
        public override async Task<Resume> GetById(Guid id)
        {
           return await Context.Resumes.AsNoTracking().Include(res => res.Student).FirstOrDefaultAsync(res => res.StudentId == id);
        }

        public override async Task<bool> HasEntity(Resume resume)
        {
            return await Context.Resumes.AnyAsync(res => res.StudentId == resume.StudentId);
        }

        public override async Task<IEnumerable<ResumeDto>> GetAll()
        {
            return await Context.Resumes.Select(res => new ResumeDto(res)
            {
                StudentName = res.Student.FirstName,
                StudentSurname = res.Student.LastName,
                StudentEmail = res.Student.Email
            }).OrderBy(res => res.ResumeId).ToListAsync();
        }
    }
}
