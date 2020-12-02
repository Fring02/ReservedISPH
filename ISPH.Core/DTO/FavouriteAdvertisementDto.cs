using System;
using ISPH.Core.Models;

namespace ISPH.Core.DTO
{
    public class FavouriteAdvertisementDto
    {
        public FavouriteAdvertisementDto()
        {
            
        }

        public FavouriteAdvertisementDto(FavouriteAdvertisement fav)
        {
            StudentId = fav.StudentId;
            AdvertisementId = fav.AdvertisementId;
        }
        public Guid StudentId { get; set; }
        public Guid AdvertisementId { get; set; }
        public string Title { get; set; }
        public uint? Salary { get; set; }
        
    }
}