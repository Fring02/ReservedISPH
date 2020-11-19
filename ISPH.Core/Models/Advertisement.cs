using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPH.Core.Models
{
    public class Advertisement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AdvertisementId { get; set; }
        public Guid EmployerId { get; set; }
        public Employer Employer { get; set; }
        public string Title { get; set; }
        public uint Salary { get; set; }
        public string Description { get; set; }
        public Guid PositionId { get; set; }
        public Position Position { get; set; }

    }
}
