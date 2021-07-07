using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ISPH.API.Responses;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Dtos;
using ISPH.Domain.Interfaces.Services;
using ISPH.Domain.Models.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.API.Controllers
{
    [Route("/api/v1/students/{studentId}/[controller]/")]
    [ApiController]
    public class FeaturedController : ControllerBase
    {
        private readonly IFeaturedAdvertisementsService _featuredService;
        private readonly IMapper _mapper;
        public FeaturedController(IFeaturedAdvertisementsService featuredService, IMapper mapper)
        {
            _featuredService = featuredService;
            _mapper = mapper;
        }


        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<FeaturedAdvertisementViewDto>> GetFavourites(Guid studentId)
        {
            return _mapper.Map<IEnumerable<FeaturedAdvertisementViewDto>>(await _featuredService.GetFavouritesAsync(studentId)
            ?? new List<FeaturedAdvertisement>());
        }


        [HttpGet("{id}")]
        public async Task<FeaturedAdvertisementViewDto> GetFeaturedById(Guid id)
        {
            var ad = await _featuredService.GetByIdAsync(id);
            if (ad == null) return null;
            return _mapper.Map<FeaturedAdvertisementViewDto>(ad);
        }

        [HttpPost]
        public async Task<IActionResult> AddToFeatured([FromBody] FeaturedAdvertisementCreateDto dto)
        {
            var fav = _mapper.Map<FeaturedAdvertisement>(dto);
            try
            {
                await _featuredService.CreateAsync(fav);
                return Ok("Advertisement with id " + dto.AdvertisementId + " added to featured");
            }
            catch (EntityPresentException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return this.ServerError("Failed to add to featured");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFromFeatured(Guid id)
        {
            var fav = await _featuredService.GetByIdAsync(id);
            if (fav == null) return BadRequest("Featured with id " + id + " doesn't exist");
            try
            {
                await _featuredService.DeleteAsync(fav);
                return Ok("Advertisement with id " + fav.AdvertisementId + " deleted from featured");
            }
            catch
            {
                return this.ServerError("Failed to delete from featured by id " + id);
            }
        }
    }
}
