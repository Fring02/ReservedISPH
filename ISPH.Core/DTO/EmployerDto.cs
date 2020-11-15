﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Core.DTO
{
    public class EmployerDTO
    {
        public int EmployerId { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
        [Required]
        [MaxLength(40)]
        public string Email { get; set; }
        [Required]
        public string CompanyName { get; set; }
    }
}
