using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Interfaces
{
    public interface IMoviesService : IDisposable
    {
        Task<IEnumerable<Movie>> GetAll();
        Task<Movie> GetById(int id);
        Task<Movie> Add(Movie movie);
        Task<Movie> Update(Movie movie);
        Task<bool> Remove(Movie movie);
        Task<IEnumerable<Movie>> GetMoviesByCategory(int categoryId);
        Task<IEnumerable<Movie>> GetMoviesByDirector(int directorId);
        Task<Movie> GetByName(string Name);
        Task<IEnumerable<Movie>> Search(string movieName);
        Task<IEnumerable<Movie>> SearchMoviesWithCategoryAndDirector(string searchedValue);
    }
}
