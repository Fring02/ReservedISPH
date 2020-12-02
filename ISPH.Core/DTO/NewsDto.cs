using ISPH.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Core.DTO
{
    public class NewsDto
    {
        public NewsDto()
        {

        }
        public NewsDto(News news)
        {
            NewsId = news.NewsId;
            Title = news.Title;
            PublishDate = news.PublishDate;
            Description = news.Description;
            ImagePath = news.ImagePath;
        }
        public Guid NewsId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime? PublishDate { get; set; }
        public string PublishDateString => PublishDate?.ToString("D", CultureInfo.CurrentCulture);
        [Required]
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public string ImagePath { get; set; }
    }
}
