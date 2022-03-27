using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.Infrastructure.Mappings
{
    class ListMapping
    {
        public void Configure(EntityTypeBuilder<List> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(b => b.Description)
                .IsRequired(false)
                .HasColumnType("varchar(100)");

            builder.Property(b => b.UserId)
                .IsRequired();

            builder.HasMany(b => b.MovieLists)
               .WithOne(l => l.List)
               .HasForeignKey(l => l.ListId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Lists");
        }
    }
}
