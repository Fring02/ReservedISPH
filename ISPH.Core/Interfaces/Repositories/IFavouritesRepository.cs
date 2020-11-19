﻿using ISPH.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ISPH.Core.Interfaces.Repositories
{
    public interface IFavouritesRepository
    {
        Task<bool> AddToFavourites(FavouriteAdvertisement ad);
        Task<bool> DeleteFromFavourites(FavouriteAdvertisement ad);
        Task<FavouriteAdvertisement> GetById(Guid studentId, Guid adId);
        Task<IList<FavouriteAdvertisement>> GetFavourites(Guid id);
    }
}
