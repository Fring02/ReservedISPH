using ISPH.Core.DTO;
using ISPH.Core.DTO.Filter;
using ISPH.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ISPH.Core.Interfaces.Repositories
{
    public interface IArticlesRepository : IEntityRepository<Article, ArticleDto>
    {
        Task<IEnumerable<ArticleDto>> GetArticles(int amount);
        Task<IEnumerable<ArticleDto>> GetFilteredArticles(FilteredNewsOrArticleDto dto);
        Task<int> GetMinArticlePublicationYear();
    }
}
