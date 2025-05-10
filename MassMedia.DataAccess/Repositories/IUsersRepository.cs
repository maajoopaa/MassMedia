using MassMedia.DataAccess.Entities;

namespace MassMedia.DataAccess.Repositories
{
    public interface IUsersRepository
    {
        Task AddAsync(UserEntity entity);
        Task DeleteAsync(UserEntity entity);
        Task<UserEntity?> GetAsync(Guid id);
        Task<List<UserEntity>> GetListAsync();
        Task UpdateAsync(UserEntity entity);
    }
}