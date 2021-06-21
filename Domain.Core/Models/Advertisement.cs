using System;
using System.ComponentModel.DataAnnotations;
using ISPH.Domain.Interfaces.Core;

namespace ISPH.Domain.Core.Models
{
    public class Advertisement : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        public Guid EmployerId { get; set; }
        public Employer Employer { get; set; }
        public string Title { get; set; }
        public uint Salary { get; set; }
        public string Description { get; set; }
        public Guid PositionId { get; set; }
        public Position Position { get; set; }
    }
}
