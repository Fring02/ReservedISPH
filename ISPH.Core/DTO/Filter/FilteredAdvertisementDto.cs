using System;
using System.Collections.Generic;
using System.Text;

namespace ISPH.Core.DTO.Filter
{
   public class FilteredAdvertisementDto
    {
        public uint? Salary { get; set; }
        public Guid CompanyId { get; set; }
        public Guid PositionId { get; set; }
    }
}
