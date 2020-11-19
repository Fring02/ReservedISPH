using System;
using ISPH.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISPH.Core.DTO
{
    public class PositionDto
    {
        public Guid PositionId { get; set; }
        [Required]
        public string Name { get; set; }
        public int Amount { get; set; }
        public IEnumerable<Advertisement> Advertisements { get; set; }
    }
}
