using ISPH.Core.DTO;
using ISPH.Core.DTO.Filter;
using ISPH.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISPH.Core.Interfaces.Repositories
{
    public interface INewsRepository : IEntityRepository<News, NewsDto>
    {
        Task<IEnumerable<NewsDto>> GetNews(int amount);
        Task<IEnumerable<NewsDto>> GetFilteredNews(FilteredNewsOrArticleDto dto);
        Task<int> GetMinNewsPublicationYear();
    }
}
