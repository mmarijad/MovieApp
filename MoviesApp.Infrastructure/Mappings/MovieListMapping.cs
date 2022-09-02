using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.Infrastructure.Mappings
{
    class MovieListMapping
    {
        public void Configure(EntityTypeBuilder<ListMovie> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.MovieId)
                 .IsRequired();

            builder.Property(b => b.ListId)
                .IsRequired();

            builder.ToTable("MovieLists");
        }
    }
}
