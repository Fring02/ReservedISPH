using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ISPH.Core.DTO;
using ISPH.Core.Models;
using ISPH.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ISPH.Infrastructure.Configuration;
using ISPH.Infrastructure.Services.Hashing;

namespace ISPH.API.Controllers.ApiControllers
{
    [Route("users/[controller]/")]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly IEmployersRepository _repos;
        private readonly DataHashService<Employer> dataService = new EmployersHashService();
        public EmployersController(IEmployersRepository repos)
        {
            _repos = repos;
        }
        [HttpGet]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IEnumerable<EmployerDto>> GetAllEmployersAsync()
        {
            return await _repos.GetAll();
        }
        [HttpGet("id={id}")]
        public async Task<EmployerDto> GetEmployerByIdAsync(Guid id)
        {
            var em = await _repos.GetById(id);
            return new EmployerDto
            {
                EmployerId = em.EmployerId,
                CompanyName = em.Company.Name,
                Email = em.Email,
                FirstName = em.FirstName,
                LastName = em.LastName,
                CompanyId = em.CompanyId
            };
        }


        [HttpPut("id={id}/update/email")]
        [Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> UpdateEmployerEmailAsync(EmployerDto em, Guid id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
                var employer = await _repos.GetById(id);
                if (employer == null) return BadRequest("This employer is not in database");
                employer.Email = em.Email;
                if (await _repos.Update(employer)) return Ok("Updated employer");
                return BadRequest("Failed to update employer");
        }

        [HttpPut("id={id}/update/company")]
        [Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> UpdateEmployerCompanyAsync(EmployerDto em, Guid id)
        {
            if (string.IsNullOrEmpty(em.CompanyName)) return BadRequest("Fill all fields");
            var employer = await _repos.GetById(id);
            if (employer == null) return BadRequest("This employer doesn't exist");
            if (await _repos.UpdateCompany(employer, em.CompanyName))
            {
                return Ok("Updated employer");
            }
            return BadRequest("Failed to update employer");
        }

        [HttpPut("id={id}/update/password")]
        [Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> UpdateEmployerPasswordAsync(EmployerDto st, Guid id)
        {
            var employer = await _repos.GetById(id);
            if (employer == null) return BadRequest("This employer doesn't exist");
            if (await _repos.UpdatePassword(employer, st.Password)) return Ok("Updated");
            return BadRequest("Failed to update employer");
        }


        [HttpPost("id={id}/confirmpassword")]
        [Authorize(Roles = RoleType.Employer)]
        public async Task<bool> ConfirmEmployerPasswordAsync(EmployerDto st, Guid id)
        {
            return dataService.CheckHashedPassword(await _repos.GetById(id), st.Password);
        }

        [HttpDelete("id={id}/delete")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> DeleteEmployerAsync(Guid id)
        {
            if(await _repos.DeleteById(id)) return Ok("Deleted employer");
            return BadRequest("Failed to delete employer");
        }
    }
}
