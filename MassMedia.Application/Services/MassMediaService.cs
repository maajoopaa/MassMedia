using MassMedia.Application.Models;
using MassMedia.DataAccess.Entities;
using MassMedia.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MassMedia.Application.Services
{
    public class MassMediaService : IMassMediaService
    {
        private readonly IMassMediasRepository _massMediasRepository;
        private readonly IUsersRepository _usersRepository;

        public MassMediaService(IMassMediasRepository massMediasRepository, IUsersRepository usersRepository)
        {
            _massMediasRepository = massMediasRepository;
            _usersRepository = usersRepository;
        }

        public async Task<ServiceResponse<List<MassMediaEntity>>> GetAllAsync()
        {
            try
            {
                var entities = await _massMediasRepository.GetListAsync();

                return new ServiceResponse<List<MassMediaEntity>>
                {
                    Success = true,
                    Data = entities
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<MassMediaEntity>>
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<List<MassMediaEntity>>> SearchAsync(string query)
        {
            try
            {
                var entities = (await _massMediasRepository.GetListAsync())
                    .Where(u => u.Title.ToLower().StartsWith(query.ToLower()))
                    .ToList();

                return new ServiceResponse<List<MassMediaEntity>>
                {
                    Success = true,
                    Data = entities
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<MassMediaEntity>>
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<List<ArticleEntity>>> GetSendedAsync(Guid id)
        {
            try
            {
                var entity = await _massMediasRepository.GetAsync(id);

                if (entity is not null)
                {
                    var articles = entity.Articles
                    .Where(x => !x.IsAccepted)
                    .ToList();

                    return new ServiceResponse<List<ArticleEntity>>
                    {
                        Success = true,
                        Data = articles
                    };
                }

                return new ServiceResponse<List<ArticleEntity>>
                {
                    Success = false,
                    Error = "СМИ не найдено"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<ArticleEntity>>
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<List<ArticleEntity>>> GetArticlesAsync(Guid id)
        {
            try
            {
                var entity = await _massMediasRepository.GetAsync(id);

                if (entity is not null)
                {
                    return new ServiceResponse<List<ArticleEntity>>
                    {
                        Success = true,
                        Data = entity.Articles
                        .Where(x => x.IsAccepted)
                        .ToList()
                    };
                }

                return new ServiceResponse<List<ArticleEntity>>
                {
                    Success = false,
                    Error = "СМИ не найдено"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<ArticleEntity>>
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<MassMediaEntity>> AddAsync(MassMediaRequest model)
        {
            try
            {
                var entity = new MassMediaEntity
                {
                    Title = model.Title,
                    ImageUrl = model.ImageUrl,
                };

                await _massMediasRepository.AddAsync(entity);

                foreach (var user in model.Workers)
                {
                    user.WorkPlaceId = entity.Id;

                    await _usersRepository.UpdateAsync(user);
                }

                return new ServiceResponse<MassMediaEntity>
                {
                    Success = true,
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<MassMediaEntity>
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }
    }
}
