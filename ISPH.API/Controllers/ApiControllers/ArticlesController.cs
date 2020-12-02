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
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesRepository _repos;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public ArticlesController(IArticlesRepository repos, IWebHostEnvironment env, IMapper mapper)
        {
            _repos = repos;
            _env = env;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ArticleDto>> GetAllArticles()
        {
           return await _repos.GetAll();
        }

        [HttpGet("minyear")]
        public async Task<int> GetMinArticlesPublicationYear()
        {
            return await _repos.GetMinArticlePublicationYear();
        }

        [HttpGet("amount={amount}")]
        public async Task<IEnumerable<ArticleDto>> GetArticlesForAmount(int amount)
        {
            return await _repos.GetArticles(amount);
        }

        [HttpPost("filter")]
        public async Task<IEnumerable<ArticleDto>> GetFilteredNews(FilteredNewsOrArticleDto dto)
        {
            return await _repos.GetFilteredArticles(dto);
        }

        [HttpGet("id={id}")]
        public async Task<ArticleDto> GetArticleById(Guid id)
        {
            var art = await _repos.GetById(id);
            return _mapper.Map<ArticleDto>(art);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddArticle([FromForm] ArticleDto art)
        {
            if(!ModelState.IsValid) return BadRequest("Fill all fields");
            string path = "/images/" + art.File.FileName;
            var article = new Article()
            {
                Title = art.Title,
                PublishDate = art.PublishDate,
                Description = art.Description,
                ImagePath = path
            };
            if (await _repos.HasEntity(article)) return BadRequest("This article already exists");
            await using(var stream = new FileStream(_env.WebRootPath + path, FileMode.Create))
            {
                await art.File.CopyToAsync(stream);
            }
            if (await _repos.Create(article)) return LocalRedirect("/home/main");

            return BadRequest("Failed to add article");
        }


        [HttpPut("id={id}/update")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> UpdateArticle(ArticleDto art, Guid id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            var article = await _repos.GetById(id);
            if (article == null) return BadRequest("This article doesn't exist");
             article.Title = art.Title;
             article.PublishDate = art.PublishDate;
             article.Description = art.Description;
            if (await _repos.Update(article)) return Ok("Updated article");

            return BadRequest("Failed to update article");
        }

        [HttpDelete("id={id}/delete")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> DeleteArticle(Guid id)
        {
            Article article = await _repos.GetById(id);
            if (article == null) return BadRequest("This article is already deleted");
            string fullPath = Path.GetFullPath("static" + article.ImagePath);
            System.IO.File.Delete(fullPath);
            if (await _repos.Delete(article)) return LocalRedirect("/home/main");
            return BadRequest("Failed to delete article");
        }
    }
}
