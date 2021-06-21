using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Dtos.Filter;
using ISPH.Domain.Interfaces.Repositories;
using ISPH.Domain.Interfaces.Services;
using ISPH.Domain.Interfaces.Services.Utils;
using ISPH.Domain.Models.Exceptions;
using ISPH.Infrastructure.Services.Utils;

namespace ISPH.Infrastructure.Services.Services
{
    public class AdvertisementsService : BaseService<IAdvertisementsRepository, Advertisement, Guid>, IAdvertisementsService
    {
        private readonly IPositionsRepository _positionsRepository;
        private readonly IEmployersRepository _employersRepository;
        private IFilterHandler<Advertisement, Guid> _filter;
        public AdvertisementsService(IAdvertisementsRepository repository, IPositionsRepository positionsRepository, IEmployersRepository employersRepository) : base(repository)
        {
            _positionsRepository = positionsRepository;
            _employersRepository = employersRepository;
            _filter = new AdvertisementFilter();
        }

        public override async Task<Advertisement> CreateAsync(Advertisement entity)
        {
            var employer = await _employersRepository.GetByIdAsync(entity.EmployerId);
            if(employer == null) throw new EntityNotFoundException("Employer not found by id " + entity.EmployerId);
            var position = await _positionsRepository.GetByIdAsync(entity.PositionId);
            if (position == null) throw new EntityNotFoundException("Position not found by id " + entity.PositionId);
            position.Amount++;
            return await base.CreateAsync(entity);
        }

        public override async Task DeleteAsync(Advertisement entity)
        {
            var position = await _positionsRepository.GetByIdAsync(entity.PositionId);
            position.Amount--;
            await base.DeleteAsync(entity);
        }

        public async Task<IEnumerable<Advertisement>> GetAdvertisementsByCompanyAsync(Guid companyId)
        {
            if (companyId == default) return null;
            return await _repository.GetAdvertisementsByCompanyAsync(companyId);
        }

        public async Task<IEnumerable<Advertisement>> GetAdvertisementsByEmployerAsync(Guid employerId)
        {
            if (employerId == default) return null;
            return await _repository.GetAdvertisementsByEmployerAsync(employerId);
        }

        public async Task<IEnumerable<Advertisement>> GetAdvertisementsByPositionAsync(Guid positionId)
        {
            if (positionId == default) return null;
            return await _repository.GetAdvertisementsByPositionAsync(positionId);
        }

        public async Task<IEnumerable<Advertisement>> GetFilteredAdvertisementsAsync(FilteredAdvertisementDto ad)
        {
            if (!string.IsNullOrEmpty(ad.Value))
            {
                _filter = _filter.With(a => a.Title.Contains(ad.Value));
            }
            else
            {
                if (ad.Salary > 0) _filter = _filter.With(a => a.Salary == ad.Salary);
                if (ad.CompanyId != default) _filter = _filter.With(a => a.Employer.CompanyId == ad.CompanyId);
                if (ad.PositionId != default) _filter = _filter.With(a => a.PositionId == ad.PositionId);
            }
            return await _repository.GetFilteredAdvertisementsAsync(_filter.Result);
        }

        public async Task<int> GetAdvertisementsCountAsync()
        {
            return await _repository.GetAdvertisementsCountAsync();
        }

        public async Task<IEnumerable<Advertisement>> GetAdvertisementsByPageAsync(int page)
        {
            if (page <= 0) return await GetAllAsync();
            return await _repository.GetAdvertisementsByPageAsync(page);
        }

        public async Task<uint> GetMaxAdvSalaryAsync()
        {
            return await _repository.GetMaxAdvSalaryAsync();
        }

        public async Task DeleteByEmployerAsync(Guid employerId)
        {
            if (employerId == default) throw new ArgumentException("Employer id is null");
            await _repository.DeleteByEmployerAsync(employerId);
        }
    }
}