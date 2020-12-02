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
        public AdvertisementDto()
        {
            
        }

        public AdvertisementDto(Advertisement adv)
        {
            AdvertisementId = adv.AdvertisementId;
            Title = adv.Title;
            Salary = adv.Salary;
            Description = adv.Description;
            PositionId = adv.PositionId;
            EmployerId = adv.EmployerId;
        }
        
        public Guid AdvertisementId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public uint? Salary { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid PositionId { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
        public string PositionName { get; set; }
        [Required]
        public Guid EmployerId { get; set; }
        public EmployerDto Employer { get; set; }
    }
}
