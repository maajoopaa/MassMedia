using FluentValidation;
using MassMedia.Application.Models;
using MassMedia.Application.Services;
using MassMedia.Application.Validators;
using MassMedia.DataAccess;
using MassMedia.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassMedia.Application
{
    public static class BuilderExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService,UserService>();
            services.AddTransient<IArticleService,ArticleService>();
            services.AddTransient<IMassMediaService,MassMediaService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IArticlesRepository, ArticlesRepository>();
            services.AddTransient<IMassMediasRepository, MassMediasRepository>();
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<UserLoginRequest>, UserLoginRequestValidator>();
            services.AddTransient<IValidator<UserRegisterRequest>, UserRegisterRequestValidator>();
            services.AddTransient<IValidator<ArticleRequest>, ArticleRequestValidator>();
            services.AddTransient<IValidator<MassMediaRequest>, MassMediaRequestValidator>();
        }

        public static void AddDb(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MassMediaDbContext>(options => options.UseLazyLoadingProxies().UseNpgsql(connectionString));
        }
    }
}
