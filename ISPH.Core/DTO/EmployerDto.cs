using ISPH.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Core.DTO
{
    public class EmployerDto
    {
        public EmployerDto()
        {

        }
        public EmployerDto(Employer e)
        {
            EmployerId = e.EmployerId;
            LastName = e.LastName;
            FirstName = e.FirstName;
            Email = e.Email;
            CompanyId = e.CompanyId;
        }
        public Guid EmployerId { get; set; }
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
        [Required]
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
