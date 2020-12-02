using ISPH.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Core.DTO
{
    public class StudentDto
    {
        public StudentDto()
        {

        }
        public StudentDto(Student s)
        {
            StudentId = s.StudentId;
            FirstName = s.FirstName;
            LastName = s.LastName;
            Email = s.Email;
        }
        public Guid StudentId { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
        [Required]
        [MaxLength(40)]
        public string Email { get; set; }

        public Guid ResumeId { get; set; }
        public string ResumeName { get; set; }
        public string ResumePath { get; set; }
    }
}
