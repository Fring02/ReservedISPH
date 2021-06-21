
using System.ComponentModel.DataAnnotations;

namespace ISPH.Domain.Dtos.Users
{
    public class StudentCreateDto : IRegisterUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}