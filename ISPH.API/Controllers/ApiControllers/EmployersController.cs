using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ISPH.Core.DTO;
using ISPH.Core.Models;
using ISPH.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ISPH.Infrastructure.Configuration;

namespace ISPH.API.Controllers.ApiControllers
{
    [Route("users/[controller]/")]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly IEmployersRepository _repos;
        public EmployersController(IEmployersRepository repos)
        {
            _repos = repos;
        }
        [HttpGet]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IEnumerable<Employer>> GetAllEmployersAsync()
        {
            return await _repos.GetAll();
        }
        [HttpGet("id={id}")]
        public async Task<Employer> GetEmployerAsync(Guid id)
        {
            return await _repos.GetById(id);
        }


        [HttpPut("id={id}/update/email")]
        [Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> UpdateEmployerEmailAsync(EmployerDto em, Guid id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
                var employer = await _repos.GetById(id);
                if (!await _repos.HasEntity(employer)) return BadRequest("This employer is not in database");
                employer.Email = em.Email;
                if (await _repos.Update(employer)) return Ok("Updated employer");
                return BadRequest("Failed to update employer");
        }

        [HttpPut("id={id}/update/company")]
        [Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> UpdateEmployerCompanyAsync(EmployerDto em, Guid id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            var employer = await _repos.GetById(id);
            if (!await _repos.HasEntity(employer)) return BadRequest("This employer is not in database");
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
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            var employer = await _repos.GetById(id);
            if (!await _repos.HasEntity(employer)) return BadRequest("This employer is not in database");
            if (await _repos.UpdatePassword(employer, st.Password)) return Ok(new { message = "Updated"});
            return BadRequest("Failed to update employer");
        }


        [HttpDelete("id={id}/delete")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> DeleteEmployerAsync(Guid id)
        {
            var employer = await _repos.GetById(id);
            if (employer == null) return BadRequest("This employer is already deleted");
            if(await _repos.Delete(employer)) return Ok("Deleted employer");
            return BadRequest("Failed to delete employer");
        }
    }
}
