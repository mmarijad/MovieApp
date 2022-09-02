using Microsoft.EntityFrameworkCore;
using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Text;
using System.Linq;

namespace MoviesApp.Infrastructure.Context
{
    public class MoviesDatabaseContext : IdentityDbContext<User>
    {
        public MoviesDatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<ListMovie> MovieLists { get; set; }
        public override DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(150)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoviesDatabaseContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlite(@"DataSource = C:\Temp\Movies_Db");
    }
}
