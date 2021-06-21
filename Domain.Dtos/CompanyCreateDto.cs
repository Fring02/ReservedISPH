using System.ComponentModel.DataAnnotations;

namespace ISPH.Domain.Dtos
{
    public class CompanyCreateDto
    {
        
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImagePath { get; set; }
    }
}