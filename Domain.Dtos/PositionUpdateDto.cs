using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ISPH.Domain.Core.Models;

namespace ISPH.Domain.Dtos
{
    public class PositionUpdateDto
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
}