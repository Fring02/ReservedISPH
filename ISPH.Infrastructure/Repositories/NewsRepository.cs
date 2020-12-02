using ISPH.Infrastructure.Data;
using ISPH.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.DTO;
using System;
using ISPH.Core.DTO.Filter;

namespace ISPH.Infrastructure.Repositories
{
    public class NewsRepository : EntityRepository<News, NewsDto>, INewsRepository
    {
        public NewsRepository(EntityContext context) : base(context)
        {
        }
        public override async Task<bool> HasEntity(News entity)
        {
            return await Context.News.AnyAsync(news => news.Title == entity.Title);
        }
        public override async Task<IEnumerable<NewsDto>> GetAll()
        {
          return await Context.News.AsNoTracking().OrderByDescending(news => news.PublishDate).Select(news => new NewsDto(news)).ToListAsync();
        }

        public override async Task<News> GetById(Guid id)
        {
           return await Context.News.AsNoTracking().FirstOrDefaultAsync(n => n.NewsId == id);
        }

        public async Task<IEnumerable<NewsDto>> GetNews(int amount)
        {
            return await Context.News.OrderByDescending(news => news.PublishDate).Take(amount).Select(news => new NewsDto(news)).ToListAsync();
        }

        public async Task<IEnumerable<NewsDto>> GetFilteredNews(FilteredNewsOrArticleDto dto)
        {
            int year = dto.Year, month = dto.Month;
            if(year > 0 && month > 0)
            return await Context.News.Where(news => news.PublishDate.Year == year && news.PublishDate.Month == month).OrderByDescending(news => news.PublishDate).
                Select(news => new NewsDto(news)).ToListAsync();
            else
            {
                if(year > 0)
                    return await Context.News.Where(news => news.PublishDate.Year == year).OrderByDescending(news => news.PublishDate).
                Select(news => new NewsDto(news)).ToListAsync();
                else if(month > 0)
                    return await Context.News.Where(news => news.PublishDate.Month == month).OrderByDescending(news => news.PublishDate).
                Select(news => new NewsDto(news)).ToListAsync();
                else
                    return await Context.News.OrderByDescending(news => news.PublishDate).
                Select(news => new NewsDto(news)).ToListAsync();
            }
        }

        public async Task<int> GetMinNewsPublicationYear()
        {
            return await Context.News.MinAsync(news => news.PublishDate.Year);
        }
    }
}
