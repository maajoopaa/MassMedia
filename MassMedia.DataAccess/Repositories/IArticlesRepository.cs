using MassMedia.DataAccess.Entities;

namespace MassMedia.DataAccess.Repositories
{
    public interface IArticlesRepository
    {
        Task AddAsync(ArticleEntity entity);
        Task DeleteAsync(ArticleEntity entity);
        Task<ArticleEntity?> GetAsync(Guid id);
        Task<List<ArticleEntity>> GetListAsync();
        Task UpdateAsync(ArticleEntity entity);
    }
}