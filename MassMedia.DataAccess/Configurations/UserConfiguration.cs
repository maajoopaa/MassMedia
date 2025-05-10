using MassMedia.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassMedia.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.HasMany(x => x.Articles)
                .WithOne(a => a.CreatedBy)
                .HasForeignKey(a => a.CreatedById);

            builder.HasOne(x => x.WorkPlace)
                .WithMany(w => w.Workers)
                .HasForeignKey(x => x.WorkPlaceId);
        }
    }
}
