using MassMedia.Application.Models;
using MassMedia.DataAccess.Entities;
using MassMedia.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MassMedia.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;

        public UserService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        private async Task<UserEntity?> GetAsync(string email, string password)
        {
            try
            {
                var user = (await _usersRepository.GetListAsync())
                    .FirstOrDefault(x => x.Email == email && x.Password == password);

                return user;
            }
            catch
            {
                return null;
            }
        }

        public async Task<UserEntity?> GetAsync(Guid id)
        {
            try
            {
                var user = (await _usersRepository.GetListAsync())
                    .FirstOrDefault(x => x.Id == id);

                return user;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ServiceResponse<UserEntity>> RegisterAsync(UserRegisterRequest model, HttpContext context)
        {
            try
            {
                if (model.Password != model.RepeatPassword)
                {
                    return new ServiceResponse<UserEntity>
                    {
                        Success = false,
                        Error = "Введенные пароли не совпадают!"
                    };
                }

                var user = new UserEntity
                {
                    Email = model.Email,
                    Password = model.Password
                };

                await _usersRepository.AddAsync(user);

                return await LoginAsync(new UserLoginRequest { Email = model.Email, Password = model.Password }, context);
            }
            catch
            {
                return new ServiceResponse<UserEntity>
                {
                    Success = false,
                    Error = "Пользователь с такой почтой уже существует!"
                };
            }
        }

        public async Task<ServiceResponse<List<UserEntity>>> SearchAsync(string query)
        {
            try
            {
                var users = (await _usersRepository.GetListAsync())
                    .Where(u => u.Email.ToLower().StartsWith(query.ToLower()) && u.WorkPlace == null)
                    .ToList();

                return new ServiceResponse<List<UserEntity>>
                {
                    Success = true,
                    Data = users
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<UserEntity>>
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<UserEntity>> LoginAsync(UserLoginRequest model, HttpContext context)
        {
            try
            {
                var user = await GetAsync(model.Email, model.Password);

                if (user is not null)
                {
                    var result = await Authorize(user, context);

                    if (result)
                    {
                        return new ServiceResponse<UserEntity>
                        {
                            Success = true,
                            Data = user
                        };
                    }

                    return new ServiceResponse<UserEntity>
                    {
                        Success = false,
                        Error = "Во время авторизации произошла ошибка! Попробуйте еще раз."
                    };
                }

                return new ServiceResponse<UserEntity>
                {
                    Success = false,
                    Error = "Почта или пароль введены неверно!"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<UserEntity>
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<bool> Authorize(UserEntity user, HttpContext context)
        {
            try
            {
                var claims = new List<Claim> {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Role, user.Role.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.NameIdentifier,
                    ClaimTypes.Role
                );

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await context.SignInAsync(claimsPrincipal);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
