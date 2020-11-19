using ISPH.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Core.DTO
{
    public class AdvertisementDto
    {
        public Guid AdvertisementId { get; set; }
        public Guid EmployerId { get; set; }
        public Employer Employer { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public uint? Salary { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid PositionId { get; set; }
        public string PositionName { get; set; }
    }
}
