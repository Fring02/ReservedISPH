using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ISPH.API.Controllers.Auth;
using ISPH.API.Responses;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Dtos.Users;
using ISPH.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ISPH.API.Controllers
{
    [Route("/api/v1/[controller]")]
    //[Authorize(Roles = RoleType.Admin)]
    [ApiController]
    public class EmployersController : AuthenticationController<Employer, EmployerCreateDto, Guid>
    {
        private readonly IEmployersService _employersService;
        public EmployersController(IEmployersService employersService, IMapper mapper,
            IConfiguration configuration) : base(employersService, configuration, mapper)
        {
            _employersService = employersService;
        }


        [HttpGet]
        public async Task<IEnumerable<EmployerViewDto>> GetAllEmployersAsync()
        {
            return _mapper.Map<IEnumerable<EmployerViewDto>>(await _employersService.GetAllAsync()
            ?? new List<Employer>());
        }
        [HttpGet("{id}")]
        public async Task<EmployerViewDto> GetEmployerByIdAsync(Guid id)
        {
            var employer = await _employersService.GetByIdAsync(id);
            if (employer == null) return null;
            return _mapper.Map<EmployerViewDto>(employer);
        }


        [HttpPut("{id}")]
        //[Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> UpdateEmployerAsync([FromBody] EmployerUpdateDto em, Guid id)
        {
            if (string.IsNullOrEmpty(em.Email) && string.IsNullOrEmpty(em.Password))
                return BadRequest("Required at least 1 field to update");
            var employer = await _employersService.GetByIdAsync(id);
            if (employer == null) return BadRequest("This employer doesn't exist");
            try
            {
                if(!string.IsNullOrEmpty(em.Email))
                    employer.Email = em.Email;
                if (!string.IsNullOrEmpty(em.Password))
                    await _employersService.UpdatePassword(employer, em.Password);
                else
                    await _employersService.UpdateAsync(employer);
                
                return Ok("Employer with id " + id + " updated");
            }
            catch
            {
                return this.ServerError("Failed to update employer");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployerAsync(Guid id)
        {
            var employer = await _employersService.GetByIdAsync(id);
            if (employer == null) return null;
            try
            {
                await _employersService.DeleteAsync(employer);
                return Ok("Employer with id " + id + " deleted");
            }
            catch
            {
                return BadRequest("Failed to delete employer");
            }
        }
    }
}
