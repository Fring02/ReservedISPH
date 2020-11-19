using System;
using System.ComponentModel.DataAnnotations;

namespace ISPH.Core.Models
{
    public class Resume
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Path { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
