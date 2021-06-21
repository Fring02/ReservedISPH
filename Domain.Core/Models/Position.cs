using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ISPH.Domain.Interfaces.Core;

namespace ISPH.Domain.Core.Models
{
    public class Position : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string ImagePath { get; set; }
        public IEnumerable<Advertisement> Advertisements { get; set; }
    }
}
