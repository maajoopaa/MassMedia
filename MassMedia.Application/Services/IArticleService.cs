using MassMedia.Application.Models;
using MassMedia.DataAccess.Entities;
using Microsoft.AspNetCore.Http;

namespace MassMedia.Application.Services
{
    public interface IArticleService
    {
        Task<ServiceResponse<Guid>> ChangeStatusAsync(Guid id, bool accept);
        Task<ServiceResponse<Guid>> DeleteAsync(Guid id);
        Task<ArticleEntity?> GetAsync(Guid id);
        Task<ServiceResponse<ArticleEntity>> SendAsync(ArticleRequest model, HttpContext context);
        Task<ServiceResponse<ArticleEntity>> UpdateAsync(Guid id, ArticleRequest model);
    }
}