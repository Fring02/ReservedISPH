﻿using System;
using ISPH.Infrastructure.Data;
using ISPH.Core.Models;
using ISPH.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ISPH.Infrastructure.Repositories
{
    public class ResumesRepository : EntityRepository<Resume>, IResumesRepository
    {
        public ResumesRepository(EntityContext context) : base(context)
        {
        }
        public override async Task<Resume> GetById(Guid id)
        {
            return await Context.Resumes.AsNoTracking().FirstOrDefaultAsync(res => res.StudentId == id);
        }

        public override async Task<bool> HasEntity(Resume resume)
        {
            return await Context.Resumes.AnyAsync(res => res.StudentId == resume.StudentId);
        }

        public override async Task<IEnumerable<Resume>> GetAll()
        {
            return await Context.Resumes.Include(res => res.Student).
                OrderBy(res => res.Id).ToListAsync();
        }
    }
}
