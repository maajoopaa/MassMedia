using MassMedia.DataAccess.Configurations;
using MassMedia.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassMedia.DataAccess
{
    public class MassMediaDbContext  : DbContext
    {
        public MassMediaDbContext(DbContextOptions<MassMediaDbContext> options)
            : base(options) { }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<ArticleEntity> Articles { get; set; }

        public DbSet<MassMediaEntity> MassMedias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
            modelBuilder.ApplyConfiguration(new MassMediaConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
