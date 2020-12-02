using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ISPH.Core.DTO;
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.Models;
using ISPH.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authorization;
using ISPH.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using ISPH.Core.DTO.Filter;

namespace ISPH.API.Controllers.ApiControllers
{
    [Route("{controller}/")]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly IAdvertisementsRepository _repos;
        private readonly IPositionsRepository _positionRepos;
        public AdvertisementsController(IAdvertisementsRepository advRepos, IPositionsRepository positionRepos)
        {
            _repos = advRepos;
            _positionRepos = positionRepos;
        }

        [HttpGet]
        public async Task<IEnumerable<AdvertisementDto>> GetAllAdvertisements()
        {
            return await _repos.GetAll();
        }


        [HttpGet("page={page}")]
        public async Task<IEnumerable<AdvertisementDto>> GetAdvertisementsByPage(int page)
        {
           return await _repos.GetAdvertisementsPerPage(page);
        }

        [HttpGet("count")]
        public async Task<int> GetAdvertisementsCount()
        {
            return await _repos.GetAdvertisementsCount();
        }

        [HttpGet("amount={amount}")]
        public async Task<IEnumerable<AdvertisementDto>> GetAdvertisementsAmount(int amount)
        {
            return await _repos.GetAdvertisementsAmount(amount);
        }

        [HttpGet("pos={id}")]
        public async Task<IEnumerable<AdvertisementDto>> GetAdvertisementsForPosition(Guid id)
        {
            return await _repos.GetAdvertisementsByEntityId(id, EntityType.Position);
        }
        [HttpGet("emp={id}")]
        public async Task<IEnumerable<AdvertisementDto>> GetAdvertisementsByEmployer(Guid id)
        {
            return await _repos.GetAdvertisementsByEntityId(id, EntityType.Employer);
        }
        [HttpGet("com={id}")]
        public async Task<IEnumerable<AdvertisementDto>> GetAllAdvertisementsForCompany(Guid id)
        {
            return await _repos.GetAdvertisementsByEntityId(id, EntityType.Company);
        }
        [HttpGet("search={value}")]
        public async Task<IEnumerable<AdvertisementDto>> GetAdvertisementsForSearchValue(string value)
        {
            value = value.ToLower();
            value = char.ToUpper(value[0]) + value.Substring(1);
            return await _repos.GetFilteredAdvertisements(value);
        }
        [HttpPost("filter")]
        public async Task<IEnumerable<AdvertisementDto>> GetFilteredAdvertisements(FilteredAdvertisementDto dto)
        {
            return await _repos.GetFilteredAdvertisements(dto);
        }
        [HttpGet("maxsalary")]
        public async Task<uint> GetMaxAdvertisementSalary()
        {
            return await _repos.GetMaxAdvSalary();
        }


        [HttpPost("emp={id}/add")]
        [Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> AddAdvertisement(AdvertisementDto adv, Guid id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            var pos = await _positionRepos.GetPositionByName(adv.PositionName);
            if (pos == null) return BadRequest("Such position is not in database");
            var advertisement = new Advertisement()
            {
                Title = adv.Title,
                Salary = adv.Salary.GetValueOrDefault(),
                Description = adv.Description,
                PositionId = pos.PositionId,
                EmployerId = id,
            };
            if (await _repos.HasEntity(advertisement)) return BadRequest("Ads with this title already exists");
            if (await _repos.Create(advertisement)) return Ok("Added new ads");
            return BadRequest("Failed to add ads");
        }


        [HttpGet("id={id}")]
        public async Task<AdvertisementDto> GetAdvertisementById(Guid id)
        {
            var ad = await _repos.GetById(id);
            return new AdvertisementDto(ad)
            {
                CompanyId = ad.Employer.CompanyId,
                Employer = new EmployerDto {CompanyName = ad.Employer.Company.Name, FirstName = ad.Employer.FirstName, LastName = ad.Employer.LastName,
                Email = ad.Employer.Email},
                PositionName = ad.Position.Name,
            };
        }

        [HttpPut("id={id}/update/title")]
        [Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> UpdateAdvertisementTitle(AdvertisementDto dto, Guid id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");

            var ad = await _repos.GetById(id);
            if (ad == null) return BadRequest("This ads is not in database");
            ad.Title = dto.Title;
            if (await _repos.Update(ad)) return Ok("Updated ads");
            return BadRequest("This ads is not in database");
        }

        [HttpPut("id={id}/update/description")]
        [Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> UpdateAdvertisementDescription(AdvertisementDto dto, Guid id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");

            var ad = await _repos.GetById(id);
            if (ad == null) return BadRequest("This ads is not in database");
            ad.Description = dto.Description;
            if (await _repos.Update(ad)) return Ok("Updated ads");
            return BadRequest("This ads is not in database");
        }

        [HttpPut("id={id}/update/salary")]
        [Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> UpdateAdvertisementSalary(AdvertisementDto dto, Guid id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");

            var ad = await _repos.GetById(id);
            if (ad == null) return BadRequest("This ads is not in database");
            ad.Salary = dto.Salary.Value;
            if (await _repos.Update(ad)) return Ok("Updated ads");
            return BadRequest("This ads is not in database");
        }

        [HttpPut("id={id}/update/position")]
        [Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> UpdateAdvertisementPosition(AdvertisementDto dto, Guid id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            var ad = await _repos.GetById(id);
            if (ad == null) return BadRequest("This ads is not in database");
            if (await _repos.UpdatePosition(ad, dto.PositionId)) return Ok("Updated ads");
            return BadRequest("This ads is not in database");
        }

        [HttpDelete("id={id}/delete")]
        [Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> DeleteAdvertisement(Guid id)
        {
            if (await _repos.DeleteById(id)) return Ok("Deleted ads");
            return BadRequest("Failed to delete ads");
        }
    }
}
