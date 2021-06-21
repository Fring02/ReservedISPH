using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ISPH.Domain.Core.Configuration;
using ISPH.Domain.Interfaces.Core;

namespace ISPH.Domain.Core.Models
{
    public class Student : BaseUser<Guid>, IEntity<Guid>
    {
        public Resume Resume { get; set; }
        public List<FeaturedAdvertisement> FeaturedAdvertisements { get; set; }
        public override string Role { get; set; } = RoleType.Student;
    }
}
