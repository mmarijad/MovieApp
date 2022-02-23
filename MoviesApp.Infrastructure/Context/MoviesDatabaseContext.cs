﻿using Microsoft.EntityFrameworkCore;
using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MoviesApp.Infrastructure.Context
{
    public class MoviesDatabaseContext : DbContext
    {
        public MoviesDatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(150)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoviesDatabaseContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlite(@"DataSource = C:\Temp\Movies_Db");
    }
}
