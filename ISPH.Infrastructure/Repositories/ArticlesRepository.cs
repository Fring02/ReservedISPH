using ISPH.Core.DTO;
using ISPH.Core.DTO.Filter;
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.Models;
using ISPH.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories
{
    public class ArticlesRepository : EntityRepository<Article, ArticleDto>, IArticlesRepository
    {
        public ArticlesRepository(EntityContext context) : base(context)
        {
        }

        public override async Task<bool> HasEntity(Article entity)
        {
            return await Context.Articles.AnyAsync(art => art.Title == entity.Title);
        }
        public override async Task<IEnumerable<ArticleDto>> GetAll()
        {
           return await Context.Articles.AsNoTracking().OrderByDescending(art => art.PublishDate).Select(art => new ArticleDto(art)).ToListAsync();
        }

        public override async Task<Article> GetById(Guid id)
        {
            return await Context.Articles.AsNoTracking().FirstOrDefaultAsync(art => art.ArticleId == id);
        }

        public async Task<IEnumerable<ArticleDto>> GetArticles(int amount)
        {
            return await Context.Articles.AsNoTracking().OrderByDescending(art => art.PublishDate).Take(amount).Select(art => new ArticleDto(art)).ToListAsync();
        }

        public async Task<IEnumerable<ArticleDto>> GetFilteredArticles(FilteredNewsOrArticleDto dto)
        {
            int year = dto.Year, month = dto.Month;
            if(year > 0 && month > 0)
            return await Context.Articles.AsNoTracking().Where(art => art.PublishDate.Year == year && art.PublishDate.Month == month).OrderByDescending(art => art.PublishDate).
                Select(art => new ArticleDto(art)).ToListAsync();
            else
            {
                if(year > 0)
                    return await Context.Articles.AsNoTracking().Where(art => art.PublishDate.Year == year).OrderByDescending(art => art.PublishDate).
                Select(art => new ArticleDto(art)).ToListAsync();
                else if(month > 0)
                    return await Context.Articles.AsNoTracking().Where(art => art.PublishDate.Month == month).OrderByDescending(art => art.PublishDate).
                Select(art => new ArticleDto(art)).ToListAsync();
                else
                    return await Context.Articles.AsNoTracking().OrderByDescending(art => art.PublishDate).
                Select(art => new ArticleDto(art)).ToListAsync();
            }
        }

        public async Task<int> GetMinArticlePublicationYear()
        {
            return await Context.Articles.MinAsync(art => art.PublishDate.Year);
        }
    }
}
