using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ISPH.Domain.Dtos.Filter
{
   public class FilteredAdvertisementDto
    {
        public string Value { get; set; }
        public uint? Salary { get; set; }
        public Guid CompanyId { get; set; }
        public Guid PositionId { get; set; }

        public bool IsValid
        {
            get
            {
                return GetType().GetProperties().Any(p => p.GetValue(this) != null);
            }
        }
    }
}
