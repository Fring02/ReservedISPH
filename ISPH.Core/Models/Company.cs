using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPH.Core.Models
{
    public class Company
    {
        [Key]
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public IEnumerable<Employer> Employers { get; set; }
    }
}
