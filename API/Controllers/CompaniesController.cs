using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ISPH.API.Responses;
using ISPH.Domain.Core.Configuration;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Dtos;
using ISPH.Domain.Interfaces.Services;
using ISPH.Domain.Models.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ISPH.API.Controllers
{
    [Route("/api/v1/[controller]")]
    //[Authorize(Roles = RoleType.Admin)]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompaniesService _companiesService;
        private readonly IMapper _mapper;
        public CompaniesController(ICompaniesService companiesService, IMapper mapper)
        {
            _companiesService = companiesService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<CompanyElementViewDto>> GetAllCompanies()
        {
            return _mapper.Map<IEnumerable<CompanyElementViewDto>>(await _companiesService.GetAllAsync() ?? new List<Company>());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<CompanyViewDto> GetCompanyByIdAsync(Guid id)
        {
            var com = await _companiesService.GetByIdAsync(id);
            if (com == null) return null;
            return _mapper.Map<CompanyViewDto>(com);
        }
        [HttpPost]
        public async Task<IActionResult> AddCompanyAsync([FromBody] CompanyCreateDto com)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            var company = _mapper.Map<Company>(com);
            try
            {
                company = await _companiesService.CreateAsync(company);
                return Created(Request.Path, company.Id);
            }
            catch (EntityPresentException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return this.ServerError("Failed to create company");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompanyAsync([FromBody] CompanyUpdateDto com, Guid id)
        {
            if (string.IsNullOrEmpty(com.Name) && string.IsNullOrEmpty(com.ImagePath)) return BadRequest("Fill all fields");
            var company = await _companiesService.GetByIdAsync(id);
            if (company == null) return BadRequest("Company with id " + id + " doesn't exist");
            try
            {
                if(!string.IsNullOrEmpty(com.Name))
                    company.Name = com.Name;
                if(!string.IsNullOrEmpty(com.ImagePath))
                    company.ImagePath = com.ImagePath;
                await _companiesService.UpdateAsync(company);
                return Ok("Company with id " + id + " updated");
            }
            catch
            {
                return this.ServerError("Failed to update company with id " + id);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyAsync(Guid id)
        {
            var com = await _companiesService.GetByIdAsync(id);
            if (com == null) return BadRequest("Company with id " + id + " doesn't exist");
            try
            {
                await _companiesService.DeleteAsync(com);
                return Ok("Company with id " + id + " deleted");
            }
            catch
            {
                return this.ServerError("Failed to delete company with id " + id);
            }
        }
    }
}
