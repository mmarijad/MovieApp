using Microsoft.Extensions.DependencyInjection;
using MoviesApp.Domain.Interfaces;
using MoviesApp.Domain.Services;
using MoviesApp.Infrastructure.Context;
using MoviesApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MoviesDatabaseContext>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IMoviesRepository, MoviesRepository>();
            services.AddScoped<IDirectorRepository, DirectorRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IMoviesService, MoviesService>();
            services.AddScoped<IDirectorService, DirectorService>();

            return services;
        }
    }
}
