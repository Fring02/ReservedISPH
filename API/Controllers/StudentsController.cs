using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ISPH.API.Controllers.Auth;
using ISPH.API.Responses;
using ISPH.Domain.Core.Configuration;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Dtos;
using ISPH.Domain.Dtos.Users;
using ISPH.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ISPH.API.Controllers
{
    [Route("/api/v1/[controller]")]
    //[Authorize(Roles = RoleType.Admin)]
    [ApiController]
    public class StudentsController : AuthenticationController<Student, StudentCreateDto, Guid>
    {
        private readonly IStudentsService _studentsService;
        public StudentsController(IStudentsService studentsService, IMapper mapper,
            IConfiguration configuration) : base(studentsService, configuration, mapper)
        {
            _studentsService = studentsService;
        }
        [HttpGet]
        public async Task<IEnumerable<StudentViewDto>> GetAllStudents()
        {
            return _mapper.Map<IEnumerable<StudentViewDto>>(await _studentsService.GetAllAsync() ??
            new List<Student>());
        }
        [HttpGet("{id}")]
        public async Task<StudentViewDto> GetStudentByIdAsync(Guid id)
        {
            var st = await _studentsService.GetByIdAsync(id);
            if (st == null) return null;
            return _mapper.Map<StudentViewDto>(st);
        }
        
        [HttpPut("{id}")]
        //[Authorize(Roles = RoleType.Student)]
        public async Task<IActionResult> UpdateStudentAsync(StudentUpdateDto st, Guid id)
        {
            if (string.IsNullOrEmpty(st.Email) && string.IsNullOrEmpty(st.Password))
                return BadRequest("Required at least 1 field to update");
            var student = await _studentsService.GetByIdAsync(id);
            if (student == null) return BadRequest("This student doesn't exist");
            try
            {
                if(!string.IsNullOrEmpty(st.Email))
                    student.Email = st.Email;
                
                if (!string.IsNullOrEmpty(st.Password))
                    await _studentsService.UpdatePassword(student, st.Password);
                else
                    await _studentsService.UpdateAsync(student);

                return Ok("Student with id " + id + " updated");
            }
            catch
            {
                return this.ServerError("Failed to update student");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentAsync(Guid id)
        {
            var st = await _studentsService.GetByIdAsync(id);
            if (st == null) return BadRequest("Student with id " + id + " doesn't exist");
            try
            {
                await _studentsService.DeleteAsync(st);
                return Ok("Student with id " + id + " deleted");
            }
            catch
            {
                return this.ServerError("Failed to delete student");
            }
        }

    }
}
