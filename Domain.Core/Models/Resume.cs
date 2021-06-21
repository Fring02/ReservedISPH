using System;
using System.ComponentModel.DataAnnotations;
using ISPH.Domain.Interfaces.Core;

namespace ISPH.Domain.Core.Models
{
    public class Resume : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
