using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPH.Core.Models
{
    public class Position
    {
        [Key]
        public Guid PositionId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string ImagePath { get; set; }
        public IEnumerable<Advertisement> Advertisements { get; set; }
    }
}
