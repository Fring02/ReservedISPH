using System;
using System.Collections.Generic;
using ISPH.Domain.Core.Models;

namespace ISPH.Domain.Dtos
{
    public class CompanyViewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public IEnumerable<Employer> Employers { get; set; }
    }
}