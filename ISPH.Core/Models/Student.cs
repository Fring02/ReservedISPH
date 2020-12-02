using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ISPH.Core.Models.General;

namespace ISPH.Core.Models
{
    public class Student : UserModel
    {
        [Key]
        public Guid StudentId { get; set; }
        public Resume Resume { get; set; }
    }
}
