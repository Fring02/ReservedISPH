using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ISPH.Core.Models
{
    public class FavouriteAdvertisement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
