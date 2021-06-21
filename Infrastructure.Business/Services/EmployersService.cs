using System;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Interfaces.Repositories;
using ISPH.Domain.Interfaces.Services;
using ISPH.Domain.Models.Exceptions;
using ISPH.Infrastructure.Services.Utils.Hashing;

namespace ISPH.Infrastructure.Services.Services
{
    public class EmployersService : BaseService<IEmployersRepository, Employer, Guid>, IEmployersService
    {
        private readonly ICompaniesRepository _companiesRepository;
        private readonly IAdvertisementsRepository _advertisementsRepository;
        public EmployersService(IEmployersRepository repository, ICompaniesRepository companiesRepository, IAdvertisementsRepository advertisementsRepository) : base(repository)
        {
            _companiesRepository = companiesRepository;
            _advertisementsRepository = advertisementsRepository;
        }

        public async Task<Employer> Register(Employer user, string password)
        {
            var company = await _companiesRepository.GetByIdAsync(user.CompanyId);
            if (company == null) throw new EntityNotFoundException("Such company doesn't exist");
            if (await HasEntityAsync(user)) throw new UserFoundException("This user already exists");
            PasswordHasher<Employer, Guid>.CreateHashedPassword(password, out var hashedPass, out var saltPass);
            user.HashedPassword = hashedPass;
            user.SaltPassword = saltPass;
            return await CreateAsync(user);
        }

        public async Task<Employer> Login(string email, string password)
        {
            var employer = await _repository.GetByEmailAsync(email);
            if (employer != null && PasswordHasher<Employer, Guid>.CheckHashedPassword(employer, password)) return employer;
            return null;
        }

        public async Task UpdatePassword(Employer entity, string password)
        {
            PasswordHasher<Employer, Guid>.CreateHashedPassword(password, out byte[] hashedPass, out byte[] saltPass);
            entity.HashedPassword = hashedPass;
            entity.SaltPassword = saltPass;
            await UpdateAsync(entity);
        }

        public async Task UpdateCompanyAsync(Employer entity, string companyName)
        {
            var company = await _companiesRepository.GetCompanyByNameAsync(companyName);
            if (company == null) throw new EntityNotFoundException($"Company by name {companyName} not found");
            entity.CompanyId = company.Id;
            try
            {
                await Task.WhenAll(_advertisementsRepository.DeleteByEmployerAsync(entity.Id), UpdateAsync(entity));
            }
            catch
            {
                throw new DatabaseException("Failed to update employer");
            }
        }
    }
}