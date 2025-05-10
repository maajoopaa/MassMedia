using MassMedia.Application.Models;
using MassMedia.DataAccess.Entities;
using MassMedia.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MassMedia.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticlesRepository _articlesRepository;

        public ArticleService(IArticlesRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        public async Task<ArticleEntity?> GetAsync(Guid id)
        {
            var article = await _articlesRepository.GetAsync(id);

            return article;
        }

        public async Task<ServiceResponse<ArticleEntity>> SendAsync(ArticleRequest model, HttpContext context)
        {
            try
            {
                var userId = Guid.Parse(context?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);

                var article = new ArticleEntity
                {
                    Head = model.Head,
                    Body = model.Body,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    ImageUrl = model.ImageUrl,
                    PublishPlaceId = model.PublishPlaceId,
                    CreatedAt = DateTime.UtcNow,
                    CreatedById = userId
                };

                await _articlesRepository.AddAsync(article);

                return new ServiceResponse<ArticleEntity>
                {
                    Success = true,
                    Data = article
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<ArticleEntity>
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<Guid>> ChangeStatusAsync(Guid id, bool accept)
        {
            try
            {
                var article = await _articlesRepository.GetAsync(id);

                if (article is not null)
                {
                    if (!accept)
                    {
                        await _articlesRepository.DeleteAsync(article);

                        return new ServiceResponse<Guid>
                        {
                            Success = true,
                            Data = id
                        };
                    }

                    article.IsAccepted = !article.IsAccepted;

                    await _articlesRepository.UpdateAsync(article);

                    return new ServiceResponse<Guid>
                    {
                        Success = true,
                        Data = id
                    };
                }

                return new ServiceResponse<Guid>
                {
                    Success = false,
                    Error = "Новость не найдена!"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Guid>
                {
                    Success = false,
                    Error = ex.Message
                };
            }

        }

        public async Task<ServiceResponse<ArticleEntity>> UpdateAsync(Guid id, ArticleRequest model)
        {
            try
            {
                var article = await _articlesRepository.GetAsync(id);

                if (article is not null)
                {
                    article.Head = model.Head;
                    article.Body = model.Body;
                    article.Latitude = model.Latitude;
                    article.Longitude = model.Longitude;
                    article.ImageUrl = model.ImageUrl;
                    article.PublishPlaceId = model.PublishPlaceId;

                    await _articlesRepository.UpdateAsync(article);

                    return new ServiceResponse<ArticleEntity>
                    {
                        Success = true,
                        Data = article
                    };
                }

                return new ServiceResponse<ArticleEntity>
                {
                    Success = false,
                    Error = "Новость не найдена!"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<ArticleEntity>
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<Guid>> DeleteAsync(Guid id)
        {
            try
            {
                var article = await _articlesRepository.GetAsync(id);

                if (article is not null)
                {
                    await _articlesRepository.DeleteAsync(article);

                    return new ServiceResponse<Guid>
                    {
                        Success = true,
                        Data = id
                    };
                }

                return new ServiceResponse<Guid>
                {
                    Success = false,
                    Error = "Новость не найдена!"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Guid>
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }
    }
}
