using System;
using System.ComponentModel.DataAnnotations;

namespace ISPH.Domain.Dtos
{
    public class CompanyElementViewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
}