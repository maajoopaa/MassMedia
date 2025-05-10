using MassMedia.Application.Models;
using MassMedia.DataAccess.Entities;

namespace MassMedia.Application.Services
{
    public interface IMassMediaService
    {
        Task<ServiceResponse<MassMediaEntity>> AddAsync(MassMediaRequest model);
        Task<ServiceResponse<List<MassMediaEntity>>> GetAllAsync();
        Task<ServiceResponse<List<ArticleEntity>>> GetArticlesAsync(Guid id);
        Task<ServiceResponse<List<ArticleEntity>>> GetSendedAsync(Guid id);
        Task<ServiceResponse<List<MassMediaEntity>>> SearchAsync(string query);
    }
}