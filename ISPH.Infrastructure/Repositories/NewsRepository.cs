﻿using ISPH.Core.Data;
using ISPH.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories
{
    public class NewsRepository : IEntityRepository<News>
    {
        private readonly EntityContext _context;
        public NewsRepository(EntityContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(News entity)
        {
            _context.News.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> HasEntity(News entity)
        {
            return await _context.News.AnyAsync(News => News.Title == entity.Title);
        }
        public async Task<bool> Delete(News entity)
        {
            _context.News.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IList<News>> GetAll()
        {
          return await _context.News.AsQueryable().OrderBy(news => news.PublishDate).ToListAsync();
        }

        public async Task<News> GetById(int id)
        {
            return await _context.News.FindAsync(id);
        }

        public bool Update(News entity)
        {
            _context.News.Update(entity);
            return _context.SaveChanges() > 0;
        }
    }
}
