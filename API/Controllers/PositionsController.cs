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
    [Route("/api/v1/[controller]")]
    //[Authorize(Roles = RoleType.Admin)]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionsService _positionsService;
        private readonly IMapper _mapper;
        public PositionsController(IPositionsService positionsService, IMapper mapper)
        {
            _positionsService = positionsService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<PositionElementViewDto>> GetAllPositionsAsync()
        {
            return _mapper.Map<IEnumerable<PositionElementViewDto>>(await _positionsService.GetAllAsync() ?? new List<Position>());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<PositionViewDto> GetPositionByIdAsync(Guid id)
        {
            var pos = await _positionsService.GetByIdAsync(id);
            if (pos == null) return null;
            return _mapper.Map<PositionViewDto>(pos);
        }

        [HttpPost]
        public async Task<IActionResult> AddPositionAsync(PositionCreateDto pos)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            var position = _mapper.Map<Position>(pos);
            try
            {
                position = await _positionsService.CreateAsync(position);
                return Created(Request.Path, position.Id);
            }
            catch (EntityPresentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return this.ServerError("Failed to create position");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePositionAsync(PositionUpdateDto pos, Guid id)
        {
            if (string.IsNullOrEmpty(pos.Name) && string.IsNullOrEmpty(pos.ImagePath))
                return BadRequest("Required at least 1 field to update");
            var position = await _positionsService.GetByIdAsync(id);
            if (position == null) return BadRequest("Position with id " + id + " doesn't exist");
            if(!string.IsNullOrEmpty(pos.Name))
                position.Name = pos.Name;
            if(!string.IsNullOrEmpty(pos.ImagePath))
                position.ImagePath = pos.ImagePath;
            try
            {
                await _positionsService.UpdateAsync(position);
                return Ok("Position with id " + id + " updated");
            }
            catch
            {
                return this.ServerError("Failed to update position with id " + id);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePositionAsync(Guid id)
        {
            var pos = await _positionsService.GetByIdAsync(id);
            if (pos == null) return BadRequest("Position with id " + id + " doesn't exist");
            try
            {
                await _positionsService.DeleteAsync(pos);
                return Ok("Position with id " + id + " deleted");
            }
            catch
            {
                return this.ServerError("Failed to delete position with id " + id);
            }
        }
    }
}
