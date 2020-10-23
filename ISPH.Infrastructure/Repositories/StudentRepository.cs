﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISPH.Core.Data;
using ISPH.Core.Models;
using ISPH.Infrastructure.Repositories.Interfaces;
using ISPH.Infrastructure.Repositories.Services;
using Microsoft.EntityFrameworkCore;

namespace ISPH.Infrastructure.Repositories
{
    public class StudentRepository : IEntityRepository<Student>, IUserAuthRepository<Student>
    {
        private readonly EntityContext _context;
        private readonly DataHashService<Student> hashService = new StudentsHashService();
        public StudentRepository(EntityContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Student entity)
        {
            _context.Students.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Student entity)
        {
            _context.Students.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<Student> GetByEmail(string email)
        {
            return await _context.Students.FirstOrDefaultAsync(student => student.Email == email);
        }
        public async Task<bool> HasEntity(Student entity)
        {
            return await _context.Students.AnyAsync(Student => Student.Email == entity.Email);
        }
        public async Task<IList<Student>> GetAll()
        {
           return await _context.Students.AsQueryable().Include(student => student.Resume).
                OrderBy(st => st.StudentId).ToListAsync();
        }

        public async Task<Student> GetById(int id)
        {
            var student = await _context.Students.FindAsync(id);
            student.Resume = await _context.Resumes.FirstOrDefaultAsync(res => res.StudentId == student.StudentId);
            return student;
        }

        public bool Update(Student entity)
        {
            _context.Students.Update(entity);
            return _context.SaveChanges() > 0;
        }

        public bool UpdatePassword(Student student, string password)
        {
            hashService.CreateHashedPassword(password, out byte[] hashedPass, out byte[] saltPass);
            student.HashedPassword = hashedPass;
            student.SaltPassword = saltPass;
            return Update(student);
        }
        //Auth

        public async Task<Student> Register(Student user, string password)
        {
            hashService.CreateHashedPassword(password, out byte[] hashedPass, out byte[] SaltPass);
            user.HashedPassword = hashedPass;
            user.SaltPassword = SaltPass;
            if (await Create(user)) return await GetByEmail(user.Email);
            return null;
        }

        public async Task<Student> Login(string email, string password)
        {
            var user = await GetByEmail(email);
            if (user != null) {
                if (hashService.CheckHashedPassword(user, password)) return user;
            }
            return null;
        }

        public async Task<bool> UserExists(Student user)
        {
            return await _context.Students.AnyAsync(st => st.Email == user.Email);
        }
    }
}
