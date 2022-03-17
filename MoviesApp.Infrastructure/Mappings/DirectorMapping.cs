using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MoviesApp.Infrastructure.Mappings
{
    public class DirectorMapping : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasColumnType("varchar(50)");

            // Jedan direktor ima više filmova
            builder.HasMany(c => c.Movies)
                .WithOne(b => b.Director)
                .HasForeignKey(b => b.DirectorId);

            builder.ToTable("Directors");
        }
    }
}
