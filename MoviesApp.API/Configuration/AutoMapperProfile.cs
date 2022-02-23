using AutoMapper;
using MoviesApp.API.DTOs.Category;
using MoviesApp.API.DTOs.Director;
using MoviesApp.API.DTOs.Movie;
using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.API.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //User mappings
            CreateMap<Category, CategoryAddDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            CreateMap<Category, CategoryResultDto>().ReverseMap();

            CreateMap<Movie, MovieAddDto>().ReverseMap();
            CreateMap<Movie, MovieUpdateDto>().ReverseMap();
            CreateMap<Movie, MovieResultDto>().ReverseMap();

            CreateMap<Director, DirectorAddDto>().ReverseMap();
            CreateMap<Director, DirectorUpdateDto>().ReverseMap();
            CreateMap<Director, DirectorResultDto>().ReverseMap();
        }
    }
}
