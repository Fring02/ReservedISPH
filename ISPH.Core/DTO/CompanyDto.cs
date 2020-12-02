using ISPH.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Core.DTO
{
    public class CompanyDto
    {
        public CompanyDto()
        {

        }
        public CompanyDto(Company c)
        {
            CompanyId = c.CompanyId;
            Name = c.Name;
            ImagePath = c.ImagePath;
        }
        public Guid CompanyId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImagePath { get; set; }
    }
}
