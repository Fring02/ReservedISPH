using System;

namespace ISPH.Domain.Dtos
{
    public class FeaturedAdvertisementViewDto
    {
        public Guid StudentId { get; set; }
        public StudentViewDto Student { get; set; }
        public Guid AdvertisementId { get; set; }
        public AdvertisementElementViewDto Advertisement { get; set; }
        public string Title { get; set; }
        public uint Salary { get; set; }
    }
}