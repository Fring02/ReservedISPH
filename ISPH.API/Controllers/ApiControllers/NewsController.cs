using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ISPH.Core.DTO;
using ISPH.Core.Models;
using ISPH.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ISPH.Infrastructure.Configuration;
using ISPH.Core.DTO.Filter;

namespace ISPH.API.Controllers.ApiControllers
{
    [Route("[controller]/")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _repos;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public NewsController(INewsRepository repos, IWebHostEnvironment env, IMapper mapper)
        {
            _repos = repos;
            _env = env;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<NewsDto>> GetAllNewsAsync()
        {
            return await _repos.GetAll();
        }
        [HttpGet("minyear")]
        public async Task<int> GetMinNewsPublicationYear()
        {
            return await _repos.GetMinNewsPublicationYear();
        }

        [HttpGet("amount={amount}")]
        public async Task<IEnumerable<NewsDto>> GetNewsForAmount(int amount)
        {
            return await _repos.GetNews(amount);
        }

        [HttpPost("filter")]
        public async Task<IEnumerable<NewsDto>> GetFilteredNews(FilteredNewsOrArticleDto dto)
        {
            return await _repos.GetFilteredNews(dto);
        }

        [HttpGet("id={id}")]
        public async Task<NewsDto> GetNewsByIdAsync(Guid id)
        {
            var news = await _repos.GetById(id);
            return _mapper.Map<NewsDto>(news);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewsAsync([FromForm] NewsDto dto)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            string path = "/images/" + dto.File.FileName;
            var news = new News()
            {
                Title = dto.Title,
                PublishDate = dto.PublishDate.GetValueOrDefault(),
                Description = dto.Description,
                ImagePath = path
            };
            if (await _repos.HasEntity(news)) return BadRequest("These news already exist");
            await using (var stream = new FileStream(_env.WebRootPath + path, FileMode.Create))
            {
               await dto.File.CopyToAsync(stream);
            }
            if (await _repos.Create(news)) return LocalRedirect("/home/main");

            return BadRequest("Failed to add news");
        }

        [HttpPut("id={id}/update")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> UpdateNewsAsync(NewsDto dto, Guid id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");

            var news = await _repos.GetById(id);
            if (news == null) return BadRequest("These news doesn't exist");
            news.Title = dto.Title;
            news.PublishDate = dto.PublishDate.GetValueOrDefault();
            news.Description = dto.Description;

            if (await _repos.Update(news)) return Ok("Updated news");
            return BadRequest("Failed to update news");
        }

        [HttpDelete("id={id}/delete")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> DeleteNewsAsync(Guid id)
        {
            var news = await _repos.GetById(id);
            if (news == null) return BadRequest("These news are already deleted");
            string fullPath = Path.GetFullPath("static" + news.ImagePath);
            System.IO.File.Delete(fullPath);
            if (await _repos.Delete(news)) return LocalRedirect("/home/main");
            return BadRequest("Failed to delete news");
        }
    }
}
