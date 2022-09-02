using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.Infrastructure.Mappings
{
    class UserMapping
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasMany(c => c.Lists)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("AspNetUsers");
        }
    }
}
