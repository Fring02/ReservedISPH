using System;
using System.ComponentModel.DataAnnotations;

namespace ISPH.Core.Models
{
    public class Resume
    {
        [Key]
        public Guid ResumeId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
