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

namespace ISPH.API.Controllers.ApiControllers
{
    [Route("users/[controller]/")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsRepository _repos;
        private readonly IMapper _mapper;
        public StudentsController(IStudentsRepository repos, IMapper mapper)
        {
            _repos = repos;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IEnumerable<StudentDto>> GetAllStudents()
        {
            var ads = await _repos.GetAll();
            return _mapper.Map<IEnumerable<StudentDto>>(ads);
        }
        [HttpGet("id={id}")]
        public async Task<Student> GetStudentAsync(Guid id)
        {
            return await _repos.GetById(id);
        }
        

        [HttpPut("id={id}/update/email")]
        [Authorize(Roles = RoleType.Student)]
        public async Task<IActionResult> UpdateStudentEmailAsync(StudentDto st, Guid id)
        {
            if(!ModelState.IsValid) return BadRequest("Fill all fields");
            var student = await _repos.GetById(id);
            if (student == null) return BadRequest("This student doesn't exist");
            student.Email = st.Email;
            if (await _repos.Update(student)) return Ok("Updated student");
            return BadRequest("Failed to update student");
        }

        [HttpPut("id={id}/update/password")]
        [Authorize(Roles = RoleType.Student)]
        public async Task<IActionResult> UpdateStudentPasswordAsync(StudentDto st, Guid id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            var student = await _repos.GetById(id);
            if (!await _repos.HasEntity(student)) return BadRequest("This employer is not in database");
            if (await _repos.UpdatePassword(student, st.Password)) return Ok("Updated");
            return BadRequest("Failed to update employer");
        }


        [HttpDelete("id={id}/delete")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> DeleteStudentAsync(Guid id)
        {
            var student = await _repos.GetById(id);
            if (student == null) return BadRequest("This student is already deleted");
            if (await _repos.Delete(student))
            {
                return Ok("Deleted student");
            }
            return BadRequest("Failed to delete student");
        }

    }
}
