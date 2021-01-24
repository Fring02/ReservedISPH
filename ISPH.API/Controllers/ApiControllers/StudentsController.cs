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
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsRepository _repos;
        private readonly DataHashService<Student> dataService = new StudentsHashService();
        public StudentsController(IStudentsRepository repos)
        {
            _repos = repos;
        }
        [HttpGet]
        [Authorize(Roles = RoleType.ADMIN)]
        public async Task<IEnumerable<StudentDto>> GetAllStudents()
        {
           return await _repos.GetAll();
        }
        [HttpGet("id={id}")]
        public async Task<StudentDto> GetStudentByIdAsync(Guid id)
        {
            var st = await _repos.GetById(id);
            if(st.Resume != null)
            {
                return new StudentDto(st) {
                ResumeId = st.Resume.ResumeId,
                ResumeName = st.Resume.Name,
                ResumePath = st.Resume.Path 
                };
            }
            return new StudentDto(st);
        }
        

        [HttpPut("id={id}/update/email")]
        [Authorize(Roles = RoleType.STUDENT)]
        public async Task<IActionResult> UpdateStudentEmailAsync(StudentDto st, Guid id)
        {
            var student = await _repos.GetById(id);
            if (student == null) return BadRequest("This student doesn't exist");
            student.Email = st.Email;
            if (await _repos.Update(student)) return Ok("Updated student");
            return BadRequest("Failed to update student");
        }

        [HttpPost("id={id}/confirmpassword")]
        [Authorize(Roles = RoleType.STUDENT)]
        public async Task<bool> ConfirmStudentPasswordAsync(StudentDto st, Guid id)
        {
            return dataService.CheckHashedPassword(await _repos.GetById(id), st.Password);
        }

        [HttpPut("id={id}/update/password")]
        [Authorize(Roles = RoleType.STUDENT)]
        public async Task<IActionResult> UpdateStudentPasswordAsync(StudentDto st, Guid id)
        {
            var student = await _repos.GetById(id);
            if (student == null) return BadRequest("This student doesn't exist");
            if (await _repos.UpdatePassword(student, st.Password)) return Ok("Updated");
            return BadRequest("Failed to update student");
        }


        [HttpDelete("id={id}/delete")]
        [Authorize(Roles = RoleType.ADMIN)]
        public async Task<IActionResult> DeleteStudentAsync(Guid id)
        {
            if (await _repos.DeleteById(id)) return Ok("Deleted student");
            return BadRequest("Failed to delete student");
        }

    }
}
