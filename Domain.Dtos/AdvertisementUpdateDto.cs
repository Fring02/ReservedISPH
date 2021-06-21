using System;

namespace ISPH.Domain.Dtos
{
    public class AdvertisementUpdateDto
    {
        public string Title { get; set; }
        public uint Salary { get; set; }
        public string Description { get; set; }
        public Guid PositionId { get; set; }
    }
}