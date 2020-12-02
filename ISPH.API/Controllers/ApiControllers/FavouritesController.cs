using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ISPH.Core.DTO;
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.Models;
using ISPH.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.API.Controllers.ApiControllers
{
    [Route("/users/students/id={studentId}/[controller]/")]
    [ApiController]
    public class FavouritesController : ControllerBase
    {
        private readonly IFavouritesRepository _repos;
        public FavouritesController(IFavouritesRepository repos)
        {
            _repos = repos;
        }


        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<FavouriteAdvertisementDto>> GetFavourites(Guid studentId)
        {
            return await _repos.GetFavourites(studentId);
        }


        [HttpGet("ad={adId}")]
        public async Task<FavouriteAdvertisementDto> GetFavourite(Guid studentId, Guid adId)
        {
            var ad = await _repos.GetById(studentId, adId);
            if (ad == null) return null;
            return new FavouriteAdvertisementDto(ad);
        }

        [HttpPost("ad={adId}/add")]
        public async Task<IActionResult> AddToFavourites(Guid studentId, Guid adId)
        {
            var fav = new FavouriteAdvertisement() { AdvertisementId = adId, StudentId = studentId };
            if (await _repos.AddToFavourites(fav)) return LocalRedirect("/home/advertisements/id=" + adId);
            return BadRequest("Failed to add to favourites");
        }

        [HttpDelete("ad={adId}/delete")]
        public async Task<IActionResult> DeleteFromFavourites(Guid studentId, Guid adId)
        {
            var fav = await _repos.GetById(studentId, adId);
            if (await _repos.DeleteFromFavourites(fav)) return Ok("Deleted from favourites");
            return BadRequest("Failed to add to favourites");
        }
    }
}
