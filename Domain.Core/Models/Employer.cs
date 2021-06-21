using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ISPH.Domain.Core.Configuration;
using ISPH.Domain.Interfaces.Core;

namespace ISPH.Domain.Core.Models
{
    public class Employer : BaseUser<Guid>
    {
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public IEnumerable<Advertisement> Advertisements { get; set; }

        public override string Role
        {
            get;
            set;
        } = RoleType.Employer;
    }
}
