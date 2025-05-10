using MassMedia.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassMedia.DataAccess.Repositories
{
    public class MassMediasRepository : IMassMediasRepository
    {
        private readonly MassMediaDbContext _context;

        public MassMediasRepository(MassMediaDbContext context)
        {
            _context = context;
        }

        public async Task<MassMediaEntity?> GetAsync(Guid id)
        {
            return await _context.MassMedias
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<MassMediaEntity>> GetListAsync()
        {
            return await _context.MassMedias
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(MassMediaEntity entity)
        {
            await _context.MassMedias
                .AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MassMediaEntity entity)
        {
            _context.MassMedias
                .Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(MassMediaEntity entity)
        {
            _context.MassMedias
                .Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}
