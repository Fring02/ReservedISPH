using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ISPH.Domain.Core.Models;

namespace ISPH.Domain.Dtos
{
    public class PositionCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImagePath { get; set; }
    }
}