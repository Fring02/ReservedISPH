using System;

namespace ISPH.Domain.Dtos.Users
{
    public class EmployerViewDto
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public Guid CompanyId { get; set; }
        public CompanyElementViewDto Company { get; set; }
    }
}