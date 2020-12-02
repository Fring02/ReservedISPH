using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ISPH.Core.Models.General;

namespace ISPH.Core.Models
{
    public class Employer : UserModel
    {
        [Key]
        public Guid EmployerId { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public IEnumerable<Advertisement> Advertisements { get; set; }

    }
}
