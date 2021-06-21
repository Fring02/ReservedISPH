using System;

namespace ISPH.Domain.Dtos
{
    public class AdvertisementElementViewDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public uint Salary { get; set; }
        public Guid PositionId { get; set; }
        public Guid EmployerId { get; set; }
    }
}