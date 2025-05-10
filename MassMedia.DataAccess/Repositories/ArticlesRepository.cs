using MassMedia.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassMedia.DataAccess.Repositories
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly MassMediaDbContext _context;

        public ArticlesRepository(MassMediaDbContext context)
        {
            _context = context;
        }

        public async Task<ArticleEntity?> GetAsync(Guid id)
        {
            return await _context.Articles
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ArticleEntity>> GetListAsync()
        {
            return await _context.Articles
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(ArticleEntity entity)
        {
            await _context.Articles
                .AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ArticleEntity entity)
        {
            _context.Articles
                .Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ArticleEntity entity)
        {
            _context.Articles
                .Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}
