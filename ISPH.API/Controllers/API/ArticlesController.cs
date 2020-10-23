﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ISPH.Core.DTO;
using ISPH.Core.Models;
using ISPH.Infrastructure;
using ISPH.Infrastructure.Repositories;
using ISPH.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISPH.API.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesRepository _repos;
        public ArticlesController(IArticlesRepository repos)
        {
            _repos = repos;
        }

        [HttpGet]
        public async Task<IList<Article>> GetAllArticles()
        {
            return await _repos.GetAll();
        }

        [HttpGet("id={id}")]
        public async Task<Article> GetArticleById(int id)
        {
            return await _repos.GetById(id);
        }

        [HttpPost("add")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> AddArticle(ArticleDTO art)
        {
            if(!ModelState.IsValid) return BadRequest("Fill all fields");
            Article article = new Article()
            {
                Title = art.Title,
                PublishDate = art.PublishDate,
                Description = art.Description,
            };
            if (await _repos.HasEntity(article)) return BadRequest("This article already exists");

            if (await _repos.Create(article)) return Ok("Added new article");

            return BadRequest("Failed to add article");
        }


        [HttpPut("id={id}/update")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> UpdateArticle(ArticleDTO art, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");

            Article article = await _repos.GetById(id);
            if (article == null) return BadRequest("This article doesn't exist");

             article.Title = art.Title;
             article.PublishDate = art.PublishDate;
             article.Description = art.Description;
            if (_repos.Update(article)) return Ok("Updated article");

                return BadRequest("Failed to update article");
        }

        [HttpDelete("id={id}/delete")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            Article article = await _repos.GetById(id);
            if (article == null) return BadRequest("This article is already deleted");
            if (await _repos.Delete(article)) return Ok("Deleted article");
            return BadRequest("Failed to delete article");
        }
    }
}
