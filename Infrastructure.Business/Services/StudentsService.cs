using System;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Interfaces.Repositories;
using ISPH.Domain.Interfaces.Services;
using ISPH.Domain.Interfaces.Services.Auth;
using ISPH.Domain.Models.Exceptions;
using ISPH.Infrastructure.Services.Utils.Hashing;

namespace ISPH.Infrastructure.Services.Services
{
    public class StudentsService : BaseService<IStudentsRepository, Student, Guid>, IStudentsService
    {
        public StudentsService(IStudentsRepository repository) : base(repository)
        {
        }
        public async Task UpdatePassword(Student student, string password)
        {
            PasswordHasher<Student, Guid>.CreateHashedPassword(password, out var hashedPass, out var saltPass);
            student.HashedPassword = hashedPass;
            student.SaltPassword = saltPass;
            await UpdateAsync(student);
        }

        public async Task<Student> Register(Student user, string password)
        {
            if(await HasEntityAsync(user)) throw new UserFoundException("This user already exists");
            PasswordHasher<Student, Guid>.CreateHashedPassword(password, out var hashedPass, out var saltPass);
            user.HashedPassword = hashedPass;
            user.SaltPassword = saltPass;
            return await CreateAsync(user);
        }

        public async Task<Student> Login(string email, string password)
        {
            var user = await _repository.GetByEmailAsync(email);
            if (user != null && PasswordHasher<Student, Guid>.CheckHashedPassword(user, password)) return user;
            return null;
        }
    }
}