using MoviesApp.Domain.Interfaces;
using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Services
{
    public class MoviesService : IMoviesService
    {

        private readonly IMoviesRepository _moviesRepository;

        public MoviesService(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        public async Task<Movie> Add(Movie movie)
        {
            if (_moviesRepository.Search(b => b.Name == movie.Name).Result.Any())
                return null;

            await _moviesRepository.Add(movie);
            return movie;
        }

        public void Dispose()
        {
            _moviesRepository?.Dispose();
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _moviesRepository.GetAll();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _moviesRepository.GetById(id);
        }

        public async Task<IEnumerable<Movie>> GetMoviesByCategory(int categoryId)
        {
            return await _moviesRepository.GetMoviesByCategory(categoryId);
        }

        public async Task<IEnumerable<Movie>> GetMoviesByDirector(int directorId)
        {
            return await _moviesRepository.GetMoviesByDirector(directorId);
        }

        public async Task<Movie> GetByName(string Name)
        {
            return await _moviesRepository.GetByName(Name);
        }

        public async Task<bool> Remove(Movie movie)
        {
            await _moviesRepository.DeleteMovie(movie);
            return true;
        }

        public async Task<IEnumerable<Movie>> Search(string movieName)
        {
            return await _moviesRepository.Search(c => c.Name.Contains(movieName));
        }

        public async Task<IEnumerable<Movie>> SearchMoviesWithCategoryAndDirector(string searchedValue)
        {
            return await _moviesRepository.SearchMoviesWithCategoryAndDirector(searchedValue);
        }

        public async Task<Movie> Update(Movie movie)
        {
            if (_moviesRepository.Search(b => b.Name == movie.Name && b.Id != movie.Id).Result.Any())
                return null;

            await _moviesRepository.Update(movie);
            return movie;
        }
    }
}
