using System;
using ISPH.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISPH.Core.DTO
{
    public class PositionDto
    {
        public PositionDto()
        {

        }
        public PositionDto(Position p)
        {
            PositionId = p.PositionId;
            Name = p.Name;
            Amount = p.Amount;
            ImagePath = p.ImagePath;
        }
        public Guid PositionId { get; set; }
        [Required]
        public string Name { get; set; }
        public int Amount { get; set; }
        public string ImagePath { get; set; }
        public IEnumerable<Advertisement> Advertisements { get; set; }
    }
}
