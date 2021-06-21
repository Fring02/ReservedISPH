using System;
using System.ComponentModel.DataAnnotations;

namespace ISPH.Domain.Dtos
{
    public class FeaturedAdvertisementCreateDto
    {
        [Required]
        public Guid StudentId { get; set; }
        [Required]
        public Guid AdvertisementId { get; set; }
    }
}