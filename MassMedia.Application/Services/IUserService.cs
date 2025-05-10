using MassMedia.Application.Models;
using MassMedia.DataAccess.Entities;
using Microsoft.AspNetCore.Http;

namespace MassMedia.Application.Services
{
    public interface IUserService
    {
        Task<bool> Authorize(UserEntity user, HttpContext context);
        Task<UserEntity?> GetAsync(Guid id);
        Task<ServiceResponse<UserEntity>> LoginAsync(UserLoginRequest model, HttpContext context);
        Task<ServiceResponse<UserEntity>> RegisterAsync(UserRegisterRequest model, HttpContext context);
        Task<ServiceResponse<List<UserEntity>>> SearchAsync(string query);
    }
}