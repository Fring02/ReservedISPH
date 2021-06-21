using System;
using System.ComponentModel.DataAnnotations;
using ISPH.Domain.Core.Models;

namespace ISPH.Domain.Dtos
{
    public class AdvertisementCreateDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public uint Salary { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid PositionId { get; set; }
        [Required]
        public Guid EmployerId { get; set; }
    }
}