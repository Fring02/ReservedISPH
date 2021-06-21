using System;
using System.ComponentModel.DataAnnotations;

namespace ISPH.Domain.Dtos
{
    public class StudentViewDto
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public ResumeDto Resume { get; set; }
    }
}