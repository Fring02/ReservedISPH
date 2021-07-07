using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ISPH.API.Responses;
using ISPH.Domain.Core.Configuration;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Dtos;
using ISPH.Domain.Dtos.Filter;
using ISPH.Domain.Interfaces.Services;
using ISPH.Domain.Models.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.API.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly IAdvertisementsService _advertisementsService;
        private readonly IMapper _mapper;
        public AdvertisementsController(IAdvertisementsService repos, IMapper mapper)
        {
            _advertisementsService = repos;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<AdvertisementElementViewDto>> GetAdvertisements([FromQuery] FilteredAdvertisementDto filter, int page)
        {
            if (filter.IsValid)
            {
                var ads = await _advertisementsService.GetFilteredAdvertisementsAsync(filter);
                if (page > 0)
                {
                    return _mapper.Map<IEnumerable<AdvertisementElementViewDto>>(ads.Take(page));
                }
                return _mapper.Map<IEnumerable<AdvertisementElementViewDto>>(ads);
            }
            if (page > 0)
            {
                return _mapper.Map<IEnumerable<AdvertisementElementViewDto>>(await _advertisementsService.GetAdvertisementsByPageAsync(page) 
             ?? new List<Advertisement>());
            }
            return _mapper.Map<IEnumerable<AdvertisementElementViewDto>>(await _advertisementsService.GetAllAsync() 
             ?? new List<Advertisement>());
        }


        [HttpGet("count")]
        public async Task<int> GetAdvertisementsCount()
        {
            return await _advertisementsService.GetAdvertisementsCountAsync();
        }

        [HttpGet("pos={id}")]
        public async Task<IEnumerable<AdvertisementElementViewDto>> GetAdvertisementsForPosition(Guid id)
        {
            return _mapper.Map<IEnumerable<AdvertisementElementViewDto>>(await _advertisementsService.GetAdvertisementsByPositionAsync(id)
             ?? new List<Advertisement>());
        }
        [HttpGet("emp={id}")]
        //[Authorize(Roles = RoleType.Employer)]
        public async Task<IEnumerable<AdvertisementElementViewDto>> GetAdvertisementsByEmployer(Guid id)
        {
            return _mapper.Map<IEnumerable<AdvertisementElementViewDto>>(await _advertisementsService.GetAdvertisementsByEmployerAsync(id)
             ?? new List<Advertisement>());
        }
        [HttpGet("com={id}")]
        public async Task<IEnumerable<AdvertisementElementViewDto>> GetAllAdvertisementsForCompany(Guid id)
        {
            return _mapper.Map<IEnumerable<AdvertisementElementViewDto>>(await _advertisementsService.GetAdvertisementsByCompanyAsync(id)
           ?? new List<Advertisement>());
        }
        [HttpGet("maxsalary")]
        public async Task<uint> GetMaxAdvertisementSalary()
        {
            return await _advertisementsService.GetMaxAdvSalaryAsync();
        }
        
        //[Authorize(Roles = RoleType.Employer)]
        [HttpPost]
        public async Task<IActionResult> CreateAdvertisement([FromBody] AdvertisementCreateDto adv)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            var advertisement = _mapper.Map<Advertisement>(adv);
            try
            {
                advertisement = await _advertisementsService.CreateAsync(advertisement);
                return Created(Request.Path, advertisement.Id);
            }
            catch (EntityNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return this.ServerError("Failed to create advertisement");
            }
        }


        [HttpGet("{id}")]
        public async Task<AdvertisementViewDto> GetAdvertisementById(Guid id)
        {
            var ad = await _advertisementsService.GetByIdAsync(id);
            if (ad == null) return null;
            return _mapper.Map<AdvertisementViewDto>(ad);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> UpdateAdvertisement([FromBody] AdvertisementUpdateDto dto, Guid id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            var ad = await _advertisementsService.GetByIdAsync(id);
            if (ad == null) return BadRequest("Advertisement with id " + id + " doesn't exist");
            
            if(!string.IsNullOrEmpty(dto.Title))
                ad.Title = dto.Title;
            if(!string.IsNullOrEmpty(dto.Description))
                ad.Description = dto.Description;
            if (dto.Salary > 0)
                ad.Salary = dto.Salary;
            if (dto.PositionId != default)
                ad.PositionId = dto.PositionId;
            try
            {
                await _advertisementsService.UpdateAsync(ad);
                return Ok("Advertisement with id " + id + " updated");
            }
            catch
            {
                return this.ServerError("Failed to update advertisement with id " + id);
            }
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> DeleteAdvertisement(Guid id)
        {
            var ad = await _advertisementsService.GetByIdAsync(id);
            if(ad == null) return BadRequest("Advertisement with id " + id + " doesn't exist");
            try
            {
                await _advertisementsService.DeleteAsync(ad);
                return Ok("Advertisement with id " + id + " deleted");
            }
            catch
            {
                return this.ServerError("Failed to delete advertisement with id " + id);
            }
        }
    }
}
