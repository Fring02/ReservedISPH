using System.ComponentModel.DataAnnotations;

namespace ISPH.Domain.Dtos.Users
{
    public interface IRegisterUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}