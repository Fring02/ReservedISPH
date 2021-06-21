using System;
using System.ComponentModel.DataAnnotations;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Dtos.Users;

namespace ISPH.Domain.Dtos
{
    public class AdvertisementViewDto
    {
        public string Title { get; set; }
        public uint Salary { get; set; }
        public string Description { get; set; }
        public Guid PositionId { get; set; }
        public PositionElementViewDto Position { get; set; }
        public Guid EmployerId { get; set; }
        public EmployerViewDto Employer { get; set; }
    }
}
