using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ISPH.Domain.Core.Models;

namespace ISPH.Domain.Dtos
{
    public class PositionViewDto
    {
      public Guid Id { get; set; }
      public string Name { get; set; }
      public int Amount { get; set; }
      public string ImagePath { get; set; }
      public IEnumerable<AdvertisementViewDto> Advertisements { get; set; }
    }
}