﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ISPH.Core.DTO;
using ISPH.Core.Models;
using ISPH.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ISPH.Infrastructure.Configuration;

namespace ISPH.API.Controllers.ApiControllers
{
    [Route("[controller]/")]
    [Authorize(Roles = RoleType.Admin)]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _repos;
        private readonly IMapper _mapper;
        public CompaniesController(ICompanyRepository repos, IMapper mapper)
        {
            _repos = repos;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IList<CompanyDto>> GetAllCompanies()
        {
            var companies = await _repos.GetAll();
            return _mapper.Map<IList<CompanyDto>>(companies);
        }

        [HttpGet("id={id}")]
        [AllowAnonymous]
        public async Task<CompanyDto> GetCompanyByIdAsync(Guid id)
        {
            var com = await _repos.GetById(id);
            return _mapper.Map<CompanyDto>(com);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCompanyAsync(CompanyDto com)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            var company = new Company { Name = com.Name };
            if (await _repos.HasEntity(company)) return BadRequest("Company is already in database");
            if (await _repos.Create(company)) return Ok("Added new company");
            return BadRequest("Failed to add company");
        }

        [HttpPut("id={id}/update")]
        public async Task<IActionResult> UpdateCompanyAsync(CompanyDto com, Guid id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            var company = await _repos.GetById(id);
            if (!(await _repos.HasEntity(company))) return BadRequest("Company is not in in database");
            company.Name = com.Name;
            if (await _repos.Update(company)) return Ok("Updated company");
            return BadRequest("Failed to add company");
        }

        [HttpDelete("id={id}/delete")]
        public async Task<IActionResult> DeleteCompanyAsync(Guid id)
        {
            var company = await _repos.GetById(id);
            if (company == null) return BadRequest("This company is already deleted");
            if (await _repos.Delete(company)) return Ok("Deleted company");
            return BadRequest("Failed to delete company");
        }
    }
}
