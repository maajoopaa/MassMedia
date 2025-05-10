using MassMedia.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassMedia.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MassMediaDbContext _context;

        public UsersRepository(MassMediaDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity?> GetAsync(Guid id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UserEntity>> GetListAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(UserEntity entity)
        {
            await _context.Users
                .AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserEntity entity)
        {
            _context.Users
                .Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserEntity entity)
        {
            _context.Users
                .Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}
