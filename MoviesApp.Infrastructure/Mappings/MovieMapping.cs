using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.Infrastructure.Mappings
{
    class MovieMapping : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(b => b.Description)
                .IsRequired(false)
                .HasColumnType("varchar(350)");

            builder.Property(b => b.CategoryId)
                .IsRequired();

            builder.Property(b => b.DirectorId)
                .IsRequired();

            builder.HasMany(c => c.MovieLists)
                .WithOne(b => b.Movie)
                .HasForeignKey(b => b.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Movie");
        }
    }
}
