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
    public class MassMediaConfiguration : IEntityTypeConfiguration<MassMediaEntity>
    {
        public void Configure(EntityTypeBuilder<MassMediaEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Title)
                .IsUnique();

            builder.HasMany(x => x.Articles)
                .WithOne(a => a.PublishPlace)
                .HasForeignKey(x => x.PublishPlaceId);
        }
    }
}
