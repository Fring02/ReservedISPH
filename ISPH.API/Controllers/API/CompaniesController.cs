﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ISPH.Core.DTO;
using ISPH.Core.Models;
using ISPH.Infrastructure;
using ISPH.Infrastructure.Repositories;
using ISPH.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ISPH.API.Controllers
{
    [Route("[controller]/")]
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
        public async Task<IList<CompanyDTO>> GetAllCompanies()
        {
            var companies = await _repos.GetAll();
            return _mapper.Map<IList<CompanyDTO>>(companies);
        }

        [HttpGet("id={id}")]
        public async Task<CompanyDTO> GetCompanyByIdAsync(int id)
        {
            var com = await _repos.GetById(id);
            return _mapper.Map<CompanyDTO>(com);
        }

        [HttpPost("add")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> AddCompanyAsync(CompanyDTO com)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            Company company = new Company { Name = com.Name };
            if (await _repos.HasEntity(company)) return BadRequest("Company is already in database");
            if (await _repos.Create(company)) return Ok("Added new company");
            return BadRequest("Failed to add company");
        }

        [HttpPut("id={id}/update")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> UpdateCompanyAsync(CompanyDTO com, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            Company company = await _repos.GetById(id);
            if (!(await _repos.HasEntity(company))) return BadRequest("Company is not in in database");
            company.Name = com.Name;
            if (_repos.Update(company)) return Ok("Updated company");
            return BadRequest("Failed to add company");
        }

        [HttpDelete("id={id}/delete")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> DeleteCompanyAsync(int id)
        {
            Company company = await _repos.GetById(id);
            if (company == null) return BadRequest("This company is already deleted");
            if (await _repos.Delete(company)) return Ok("Deleted company");
            return BadRequest("Failed to delete company");
        }
    }
}
