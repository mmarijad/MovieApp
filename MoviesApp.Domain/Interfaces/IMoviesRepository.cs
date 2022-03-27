using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Interfaces
{
    public interface IMoviesRepository : IRepository<Movie>
    {
        new Task<List<Movie>> GetAll();
        new Task<Movie> GetById(int id);
        Task<IEnumerable<Movie>> GetMoviesByCategory(int categoryId);
        Task<IEnumerable<Movie>> GetMoviesByDirector(int directorId);
        Task<Movie> GetByName(string Name);
        Task<IEnumerable<Movie>> SearchMoviesWithCategoryAndDirector(string searchedValue);
        Task<bool> DeleteMovie(Movie movie);
    }
}
