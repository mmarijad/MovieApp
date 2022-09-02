using AutoMapper;
using MoviesApp.API.DTOs.Category;
using MoviesApp.API.DTOs.Director;
using MoviesApp.API.DTOs.List;
using MoviesApp.API.DTOs.Movie;
using MoviesApp.API.DTOs.MovieList;
using MoviesApp.API.DTOs.User;
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
            CreateMap<Category, CategoryAddDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            CreateMap<Category, CategoryResultDto>().ReverseMap();

            CreateMap<Movie, MovieAddDto>().ReverseMap();
            CreateMap<Movie, MovieUpdateDto>().ReverseMap();
            CreateMap<Movie, MovieResultDto>().ReverseMap();

            CreateMap<Director, DirectorAddDto>().ReverseMap();
            CreateMap<Director, DirectorUpdateDto>().ReverseMap();
            CreateMap<Director, DirectorResultDto>().ReverseMap();

            CreateMap<ListMovie, MovieListAddDto>().ReverseMap();
            CreateMap<ListMovie, MovieListUpdateDto>().ReverseMap();
            CreateMap<ListMovie, MovieListResultDto>().ReverseMap();

            CreateMap<List, ListAddDto>().ReverseMap();
            CreateMap<List, ListUpdateDto>().ReverseMap();
            CreateMap<List, ListResultDto>().ReverseMap();

            CreateMap<User, UserRegistrationDto>().ReverseMap();
        }
    }
}
