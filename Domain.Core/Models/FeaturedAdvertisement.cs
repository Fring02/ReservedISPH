using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ISPH.Domain.Interfaces.Core;

namespace ISPH.Domain.Core.Models
{
    public class FeaturedAdvertisement : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
