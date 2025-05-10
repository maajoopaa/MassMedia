using MassMedia.DataAccess.Entities;

namespace MassMedia.DataAccess.Repositories
{
    public interface IMassMediasRepository
    {
        Task AddAsync(MassMediaEntity entity);
        Task DeleteAsync(MassMediaEntity entity);
        Task<MassMediaEntity?> GetAsync(Guid id);
        Task<List<MassMediaEntity>> GetListAsync();
        Task UpdateAsync(MassMediaEntity entity);
    }
}